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
using libc.orm.DatabaseMigration.Abstractions.Builders.Delete;
using libc.orm.DatabaseMigration.Abstractions.Builders.Delete.Column;
using libc.orm.DatabaseMigration.Abstractions.Builders.Delete.Constraint;
using libc.orm.DatabaseMigration.Abstractions.Builders.Delete.DefaultConstraint;
using libc.orm.DatabaseMigration.Abstractions.Builders.Delete.ForeignKey;
using libc.orm.DatabaseMigration.Abstractions.Builders.Delete.Index;
using libc.orm.DatabaseMigration.Abstractions.Expressions;
using libc.orm.DatabaseMigration.Abstractions.Model;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Delete.Column;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Delete.Constraint;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Delete.DefaultConstraint;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Delete.ForeignKey;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Delete.Index;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Delete.Sequence;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Delete.Table;
using libc.orm.DatabaseMigration.DdlMigration;

namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.Delete
{
    /// <summary>
    ///     The implementation of the <see cref="IDeleteExpressionRoot" /> interface
    /// </summary>
    public class DeleteExpressionRoot : IDeleteExpressionRoot
    {
        private readonly MigrationContext _context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeleteExpressionRoot" /> class.
        /// </summary>
        /// <param name="context">The migration context</param>
        public DeleteExpressionRoot(MigrationContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public void Schema(string schemaName)
        {
            var expression = new DeleteSchemaExpression
            {
                SchemaName = schemaName
            };

            _context.Expressions.Add(expression);
        }

        /// <inheritdoc />
        public IInSchemaSyntax Table(string tableName)
        {
            var expression = new DeleteTableExpression
            {
                TableName = tableName
            };

            _context.Expressions.Add(expression);

            return new DeleteTableExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IDeleteColumnFromTableSyntax Column(string columnName)
        {
            var expression = new DeleteColumnExpression
            {
                ColumnNames =
                {
                    columnName
                }
            };

            _context.Expressions.Add(expression);

            return new DeleteColumnExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IDeleteForeignKeyFromTableSyntax ForeignKey()
        {
            var expression = new DeleteForeignKeyExpression();
            _context.Expressions.Add(expression);

            return new DeleteForeignKeyExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IDeleteForeignKeyOnTableSyntax ForeignKey(string foreignKeyName)
        {
            var expression = new DeleteForeignKeyExpression
            {
                ForeignKey =
                {
                    Name = foreignKeyName
                }
            };

            _context.Expressions.Add(expression);

            return new DeleteForeignKeyExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IDeleteDataOrInSchemaSyntax FromTable(string tableName)
        {
            var expression = new DeleteDataExpression
            {
                TableName = tableName
            };

            _context.Expressions.Add(expression);

            return new DeleteDataExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IDeleteIndexForTableSyntax Index(string indexName)
        {
            var expression = new DeleteIndexExpression();
            expression.Index.Name = indexName;
            _context.Expressions.Add(expression);

            return new DeleteIndexExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IDeleteIndexForTableSyntax Index()
        {
            var expression = new DeleteIndexExpression();
            _context.Expressions.Add(expression);

            return new DeleteIndexExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IInSchemaSyntax Sequence(string sequenceName)
        {
            var expression = new DeleteSequenceExpression
            {
                SequenceName = sequenceName
            };

            _context.Expressions.Add(expression);

            return new DeleteSequenceExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IDeleteConstraintOnTableSyntax PrimaryKey(string primaryKeyName)
        {
            var expression = new DeleteConstraintExpression(ConstraintType.PrimaryKey);
            expression.Constraint.ConstraintName = primaryKeyName;
            _context.Expressions.Add(expression);

            return new DeleteConstraintExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IDeleteConstraintOnTableSyntax UniqueConstraint(string constraintName)
        {
            var expression = new DeleteConstraintExpression(ConstraintType.Unique);
            expression.Constraint.ConstraintName = constraintName;
            _context.Expressions.Add(expression);

            return new DeleteConstraintExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IDeleteConstraintOnTableSyntax UniqueConstraint()
        {
            var expression = new DeleteConstraintExpression(ConstraintType.Unique);
            _context.Expressions.Add(expression);

            return new DeleteConstraintExpressionBuilder(expression);
        }

        /// <inheritdoc />
        public IDeleteDefaultConstraintOnTableSyntax DefaultConstraint()
        {
            var expression = new DeleteDefaultConstraintExpression();
            _context.Expressions.Add(expression);

            return new DeleteDefaultConstraintExpressionBuilder(expression);
        }
    }
}