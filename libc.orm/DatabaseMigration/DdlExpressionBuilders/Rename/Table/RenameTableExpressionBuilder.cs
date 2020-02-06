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

using libc.orm.DatabaseMigration.Abstractions.Builders;
using libc.orm.DatabaseMigration.Abstractions.Builders.Rename.Table;
using libc.orm.DatabaseMigration.Abstractions.Expressions;
namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.Rename.Table {
    /// <summary>
    ///     An expression builder for a <see cref="RenameTableExpression" />
    /// </summary>
    public class RenameTableExpressionBuilder : ExpressionBuilderBase<RenameTableExpression>,
        IRenameTableToOrInSchemaSyntax,
        IInSchemaSyntax {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RenameTableExpressionBuilder" /> class.
        /// </summary>
        /// <param name="expression">The underlying expression</param>
        public RenameTableExpressionBuilder(RenameTableExpression expression)
            : base(expression) {
        }
        /// <inheritdoc />
        public void InSchema(string schemaName) {
            Expression.SchemaName = schemaName;
        }
        /// <inheritdoc />
        public IInSchemaSyntax To(string name) {
            Expression.NewName = name;
            return this;
        }
        /// <inheritdoc />
        IRenameTableToSyntax IRenameTableToOrInSchemaSyntax.InSchema(string schemaName) {
            Expression.SchemaName = schemaName;
            return this;
        }
    }
}