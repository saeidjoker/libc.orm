#region License

//
// Copyright (c) 2007-2018, Sean Chambers <schambers80@gmail.com>
// Copyright (c) 2011, Grant Archibald
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using libc.orm.DatabaseMigration.Abstractions.Builders.Alter;
using libc.orm.DatabaseMigration.Abstractions.Builders.Create;
using libc.orm.DatabaseMigration.Abstractions.Builders.Delete;
using libc.orm.DatabaseMigration.Abstractions.Builders.Execute;
using libc.orm.DatabaseMigration.Abstractions.Builders.IfDatabase;
using libc.orm.DatabaseMigration.Abstractions.Builders.Insert;
using libc.orm.DatabaseMigration.Abstractions.Builders.Rename;
using libc.orm.DatabaseMigration.Abstractions.Builders.Schema;
using libc.orm.DatabaseMigration.Abstractions.Builders.Update;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Alter;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Create;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Delete;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Execute;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Insert;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Rename;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Schema;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Update;
using libc.orm.DatabaseMigration.DdlMigration;
using libc.orm.DatabaseMigration.DdlProcessing;

namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.IfDatabase
{
    /// <summary>
    ///     Allows for conditional inclusion of expresions based on the migration context
    /// </summary>
    public class IfDatabaseExpressionRoot : IIfDatabaseExpressionRoot
    {
        /// <summary>
        ///     The context to add expressions into
        /// </summary>
        /// <remarks>If the database type doe snot apply then this will be a new context that is not used by the caller</remarks>
        private readonly MigrationContext _context;

        /// <summary>
        ///     Initializes a new instance of a the <see cref="IfDatabaseExpressionRoot" /> class that will only add expressions to
        ///     the provided <paramref name="context" /> if <paramref name="databaseType" /> matches the migration processor
        /// </summary>
        /// <remarks>
        ///     If the database type does not apply then a <seealso cref="NullIfDatabaseProcessor" /> will be used as a
        ///     container to void any fluent expressions that would have been executed
        /// </remarks>
        /// <param name="context">The context to add expressions to if the database type applies</param>
        /// <param name="databaseType">The database type that the expressions relate to</param>
        public IfDatabaseExpressionRoot(MigrationContext context, params string[] databaseType)
        {
            if (databaseType == null) throw new ArgumentNullException(nameof(databaseType));

            _context = DatabaseTypeApplies(context, databaseType)
                ? context
                : CreateEmptyMigrationContext(context);
        }

        /// <summary>
        ///     Initializes a new instance of a the <see cref="IfDatabaseExpressionRoot" /> class that will only add expressions to
        ///     the provided <paramref name="context" /> if <paramref name="databaseTypePredicate" /> is true for the migration
        ///     processor
        /// </summary>
        /// <remarks>
        ///     If the database type does not apply then a <seealso cref="NullIfDatabaseProcessor" /> will be used as a
        ///     container to void any fluent expressions that would have been executed
        /// </remarks>
        /// <param name="context">The context to add expressions to if the database type applies</param>
        /// <param name="databaseTypePredicate">The predicate that must be true for the expression to run</param>
        public IfDatabaseExpressionRoot(MigrationContext context, Predicate<string> databaseTypePredicate)
        {
            if (databaseTypePredicate == null) throw new ArgumentNullException(nameof(databaseTypePredicate));

            _context = DatabaseTypeApplies(context, databaseTypePredicate)
                ? context
                : CreateEmptyMigrationContext(context);
        }

        /// <summary>
        ///     Alter the schema of an existing object
        /// </summary>
        public IAlterExpressionRoot Alter => new AlterExpressionRoot(_context);

        /// <summary>
        ///     Create a new database object
        /// </summary>
        public ICreateExpressionRoot Create => new CreateExpressionRoot(_context);

        /// <summary>
        ///     Delete a database object, table, or row
        /// </summary>
        public IDeleteExpressionRoot Delete => new DeleteExpressionRoot(_context);

        /// <summary>
        ///     Rename tables / columns
        /// </summary>
        public IRenameExpressionRoot Rename => new RenameExpressionRoot(_context);

        /// <summary>
        ///     Insert data into a table
        /// </summary>
        public IInsertExpressionRoot Insert => new InsertExpressionRoot(_context);

        /// <summary>
        ///     Execute SQL statements
        /// </summary>
        public IExecuteExpressionRoot Execute => new ExecuteExpressionRoot(_context);

        /// <inheritdoc />
        public ISchemaExpressionRoot Schema => new SchemaExpressionRoot(_context);

        /// <summary>
        ///     Update an existing row
        /// </summary>
        public IUpdateExpressionRoot Update => new UpdateExpressionRoot(_context);

        /// <inheritdoc />
        public void Delegate(Action delegation)
        {
            if (_context.QuerySchema is NullIfDatabaseProcessor) return;
            delegation.Invoke();
        }

        /// <summary>
        ///     Checks if the database type matches the name of the context migration processor
        /// </summary>
        /// <param name="context">The context to evaluate</param>
        /// <param name="databaseTypes">The type to be checked</param>
        /// <returns><c>True</c> if the database type applies, <c>False</c> if not</returns>
        private static bool DatabaseTypeApplies(MigrationContext context, params string[] databaseTypes)
        {
            if (context.QuerySchema is IProcessor mp)
            {
                var processorDbTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    mp.DatabaseType
                };

                foreach (var databaseType in mp.DatabaseTypeAliases)
                    processorDbTypes.Add(databaseType);

                return databaseTypes
                    .Any(db => processorDbTypes.Contains(db));
            }

            return false;
        }

        /// <summary>
        ///     Checks if the database type matches the name of the context migration processor
        /// </summary>
        /// <param name="context">The context to evaluate</param>
        /// <param name="databaseTypePredicate">The predicate to be evaluated</param>
        /// <returns><c>True</c> if the database type applies, <c>False</c> if not</returns>
        private static bool DatabaseTypeApplies(MigrationContext context, Predicate<string> databaseTypePredicate)
        {
            if (context.QuerySchema is IProcessor mp) return databaseTypePredicate(mp.DatabaseType);

            return false;
        }

        private static MigrationContext CreateEmptyMigrationContext(MigrationContext originalContext)
        {
            return new MigrationContext(new NullIfDatabaseProcessor(), string.Empty);
        }
    }
}