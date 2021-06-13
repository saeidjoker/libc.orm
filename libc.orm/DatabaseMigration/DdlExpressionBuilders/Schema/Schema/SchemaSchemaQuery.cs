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

using libc.orm.DatabaseMigration.Abstractions.Builders.Schema.Schema;
using libc.orm.DatabaseMigration.Abstractions.Builders.Schema.Table;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Schema.Table;
using libc.orm.DatabaseMigration.DdlMigration;

namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.Schema.Schema
{
    /// <summary>
    ///     The implementation of the <see cref="ISchemaSchemaSyntax" /> interface.
    /// </summary>
    public class SchemaSchemaQuery : ISchemaSchemaSyntax
    {
        private readonly MigrationContext _context;
        private readonly string _schemaName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SchemaSchemaQuery" /> class.
        /// </summary>
        /// <param name="context">The migration context</param>
        /// <param name="schemaName">The schema name</param>
        public SchemaSchemaQuery(MigrationContext context, string schemaName)
        {
            _context = context;
            _schemaName = schemaName;
        }

        /// <inheritdoc />
        public bool Exists()
        {
            return _context.QuerySchema.SchemaExists(_schemaName);
        }

        /// <inheritdoc />
        public ISchemaTableSyntax Table(string tableName)
        {
            return new SchemaTableQuery(_context, _schemaName, tableName);
        }
    }
}