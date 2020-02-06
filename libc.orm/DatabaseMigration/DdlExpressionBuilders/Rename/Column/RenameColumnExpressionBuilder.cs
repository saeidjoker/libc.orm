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
using libc.orm.DatabaseMigration.Abstractions.Builders.Rename.Column;
using libc.orm.DatabaseMigration.Abstractions.Expressions;
namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.Rename.Column {
    /// <summary>
    ///     An expression builder for a <see cref="RenameColumnExpression" />
    /// </summary>
    public class RenameColumnExpressionBuilder : ExpressionBuilderBase<RenameColumnExpression>,
        IRenameColumnToOrInSchemaSyntax,
        IRenameColumnTableSyntax {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RenameColumnExpressionBuilder" /> class.
        /// </summary>
        /// <param name="expression">The underlying expression</param>
        public RenameColumnExpressionBuilder(RenameColumnExpression expression)
            : base(expression) {
        }
        /// <inheritdoc />
        public IRenameColumnToOrInSchemaSyntax OnTable(string tableName) {
            Expression.TableName = tableName;
            return this;
        }
        /// <inheritdoc />
        public void To(string name) {
            Expression.NewName = name;
        }
        /// <inheritdoc />
        public IRenameColumnToSyntax InSchema(string schemaName) {
            Expression.SchemaName = schemaName;
            return this;
        }
    }
}