#region License

//
// Copyright (c) 2007-2018, Sean Chambers <schambers80@gmail.com>
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

using libc.orm.DatabaseMigration.Abstractions.Builders.Schema.Constraint;
using libc.orm.DatabaseMigration.DdlMigration;

namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.Schema.Constraint
{
    /// <summary>
    ///     The implementation of the <see cref="ISchemaConstraintSyntax" /> interface.
    /// </summary>
    public class SchemaConstraintQuery : ISchemaConstraintSyntax
    {
        private readonly string _constraintName;
        private readonly MigrationContext _context;
        private readonly string _schemaName;
        private readonly string _tableName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SchemaConstraintQuery" /> class.
        /// </summary>
        /// <param name="schemaName">The schema name</param>
        /// <param name="tableName">The table name</param>
        /// <param name="constraintName">The constraint name</param>
        /// <param name="context">The migration context</param>
        public SchemaConstraintQuery(string schemaName, string tableName, string constraintName,
            MigrationContext context)
        {
            _schemaName = schemaName;
            _tableName = tableName;
            _constraintName = constraintName;
            _context = context;
        }

        /// <inheritdoc />
        public bool Exists()
        {
            return _context.QuerySchema.ConstraintExists(_schemaName, _tableName, _constraintName);
        }
    }
}