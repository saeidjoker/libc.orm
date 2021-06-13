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
using libc.orm.DatabaseMigration.Abstractions.Builders.Delete.ForeignKey;
using libc.orm.DatabaseMigration.Abstractions.Expressions;

namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.Delete.ForeignKey
{
    /// <summary>
    ///     An expression builder for a <see cref="DeleteForeignKeyExpression" />
    /// </summary>
    public class DeleteForeignKeyExpressionBuilder : ExpressionBuilderBase<DeleteForeignKeyExpression>,
        IDeleteForeignKeyFromTableSyntax,
        IDeleteForeignKeyForeignColumnOrInSchemaSyntax,
        IDeleteForeignKeyToTableSyntax,
        IDeleteForeignKeyPrimaryColumnSyntax,
        IDeleteForeignKeyOnTableSyntax,
        IInSchemaSyntax
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DeleteForeignKeyExpressionBuilder" /> class.
        /// </summary>
        /// <param name="expression">The underlying expression</param>
        public DeleteForeignKeyExpressionBuilder(DeleteForeignKeyExpression expression)
            : base(expression)
        {
        }

        /// <inheritdoc />
        public IDeleteForeignKeyForeignColumnSyntax InSchema(string foreignSchemaName)
        {
            Expression.ForeignKey.ForeignTableSchema = foreignSchemaName;

            return this;
        }

        /// <inheritdoc />
        public IDeleteForeignKeyToTableSyntax ForeignColumn(string column)
        {
            Expression.ForeignKey.ForeignColumns.Add(column);

            return this;
        }

        /// <inheritdoc />
        public IDeleteForeignKeyToTableSyntax ForeignColumns(params string[] columns)
        {
            foreach (var column in columns)
                Expression.ForeignKey.ForeignColumns.Add(column);

            return this;
        }

        /// <inheritdoc />
        public IDeleteForeignKeyForeignColumnOrInSchemaSyntax FromTable(string foreignTableName)
        {
            Expression.ForeignKey.ForeignTable = foreignTableName;

            return this;
        }

        /// <inheritdoc />
        IInSchemaSyntax IDeleteForeignKeyOnTableSyntax.OnTable(string foreignTableName)
        {
            Expression.ForeignKey.ForeignTable = foreignTableName;

            return this;
        }

        /// <inheritdoc />
        public void PrimaryColumn(string column)
        {
            Expression.ForeignKey.PrimaryColumns.Add(column);
        }

        /// <inheritdoc />
        public void PrimaryColumns(params string[] columns)
        {
            foreach (var column in columns)
                Expression.ForeignKey.PrimaryColumns.Add(column);
        }

        /// <inheritdoc />
        public IDeleteForeignKeyPrimaryColumnSyntax ToTable(string table)
        {
            Expression.ForeignKey.PrimaryTable = table;

            return this;
        }

        /// <inheritdoc />
        void IInSchemaSyntax.InSchema(string schemaName)
        {
            Expression.ForeignKey.ForeignTableSchema = schemaName;
        }
    }
}