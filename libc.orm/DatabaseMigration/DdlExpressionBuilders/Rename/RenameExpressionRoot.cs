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

using libc.orm.DatabaseMigration.Abstractions.Builders.Rename;
using libc.orm.DatabaseMigration.Abstractions.Builders.Rename.Table;
using libc.orm.DatabaseMigration.Abstractions.Expressions;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Rename.Column;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Rename.Table;
using libc.orm.DatabaseMigration.DdlMigration;
namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.Rename {
    /// <summary>
    ///     The implementation of the <see cref="IRenameExpressionRoot" /> interface.
    /// </summary>
    public class RenameExpressionRoot : IRenameExpressionRoot {
        private readonly MigrationContext _context;
        /// <summary>
        ///     Initializes a new instance of the <see cref="RenameExpressionRoot" /> class.
        /// </summary>
        /// <param name="context">The migration context</param>
        public RenameExpressionRoot(MigrationContext context) {
            _context = context;
        }
        /// <inheritdoc />
        public IRenameTableToOrInSchemaSyntax Table(string oldName) {
            var expression = new RenameTableExpression {
                OldName = oldName
            };
            _context.Expressions.Add(expression);
            return new RenameTableExpressionBuilder(expression);
        }
        /// <inheritdoc />
        public IRenameColumnTableSyntax Column(string oldName) {
            var expression = new RenameColumnExpression {
                OldName = oldName
            };
            _context.Expressions.Add(expression);
            return new RenameColumnExpressionBuilder(expression);
        }
    }
}