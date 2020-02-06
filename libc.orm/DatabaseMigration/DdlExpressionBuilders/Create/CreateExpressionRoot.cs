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

using libc.orm.DatabaseMigration.Abstractions.Builders.Create;
using libc.orm.DatabaseMigration.Abstractions.Builders.Create.Column;
using libc.orm.DatabaseMigration.Abstractions.Builders.Create.Constraint;
using libc.orm.DatabaseMigration.Abstractions.Builders.Create.ForeignKey;
using libc.orm.DatabaseMigration.Abstractions.Builders.Create.Index;
using libc.orm.DatabaseMigration.Abstractions.Builders.Create.Schema;
using libc.orm.DatabaseMigration.Abstractions.Builders.Create.Sequence;
using libc.orm.DatabaseMigration.Abstractions.Builders.Create.Table;
using libc.orm.DatabaseMigration.Abstractions.Expressions;
using libc.orm.DatabaseMigration.Abstractions.Model;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Create.Column;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Create.Constraint;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Create.ForeignKey;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Create.Index;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Create.Schema;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Create.Sequence;
using libc.orm.DatabaseMigration.DdlExpressionBuilders.Create.Table;
using libc.orm.DatabaseMigration.DdlMigration;
namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.Create {
    /// <summary>
    ///     The <see cref="ICreateExpressionRoot" /> implementation
    /// </summary>
    public class CreateExpressionRoot : ICreateExpressionRoot {
        private readonly MigrationContext _context;
        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateExpressionRoot" /> class.
        /// </summary>
        /// <param name="context">The migration context</param>
        public CreateExpressionRoot(MigrationContext context) {
            _context = context;
        }
        /// <inheritdoc />
        public ICreateSchemaOptionsSyntax Schema(string schemaName) {
            var expression = new CreateSchemaExpression {
                SchemaName = schemaName
            };
            _context.Expressions.Add(expression);
            return new CreateSchemaExpressionBuilder(expression);
        }
        /// <inheritdoc />
        public ICreateTableWithColumnOrSchemaOrDescriptionSyntax Table(string tableName) {
            var expression = new CreateTableExpression {
                TableName = tableName
            };
            _context.Expressions.Add(expression);
            return new CreateTableExpressionBuilder(expression, _context);
        }
        /// <inheritdoc />
        public ICreateColumnOnTableSyntax Column(string columnName) {
            var expression = new CreateColumnExpression {
                Column = {
                    Name = columnName
                }
            };
            _context.Expressions.Add(expression);
            return new CreateColumnExpressionBuilder(expression, _context);
        }
        /// <inheritdoc />
        public ICreateForeignKeyFromTableSyntax ForeignKey() {
            var expression = new CreateForeignKeyExpression();
            _context.Expressions.Add(expression);
            return new CreateForeignKeyExpressionBuilder(expression);
        }
        /// <inheritdoc />
        public ICreateForeignKeyFromTableSyntax ForeignKey(string foreignKeyName) {
            var expression = new CreateForeignKeyExpression {
                ForeignKey = {
                    Name = foreignKeyName
                }
            };
            _context.Expressions.Add(expression);
            return new CreateForeignKeyExpressionBuilder(expression);
        }
        /// <inheritdoc />
        public ICreateIndexForTableSyntax Index() {
            var expression = new CreateIndexExpression();
            _context.Expressions.Add(expression);
            return new CreateIndexExpressionBuilder(expression);
        }
        /// <inheritdoc />
        public ICreateIndexForTableSyntax Index(string indexName) {
            var expression = new CreateIndexExpression {
                Index = {
                    Name = indexName
                }
            };
            _context.Expressions.Add(expression);
            return new CreateIndexExpressionBuilder(expression);
        }
        /// <inheritdoc />
        public ICreateSequenceInSchemaSyntax Sequence(string sequenceName) {
            var expression = new CreateSequenceExpression {
                Sequence = {
                    Name = sequenceName
                }
            };
            _context.Expressions.Add(expression);
            return new CreateSequenceExpressionBuilder(expression);
        }
        /// <inheritdoc />
        public ICreateConstraintOnTableSyntax UniqueConstraint() {
            var expression = new CreateConstraintExpression(ConstraintType.Unique);
            _context.Expressions.Add(expression);
            return new CreateConstraintExpressionBuilder(expression);
        }
        /// <inheritdoc />
        public ICreateConstraintOnTableSyntax UniqueConstraint(string constraintName) {
            var expression = new CreateConstraintExpression(ConstraintType.Unique);
            expression.Constraint.ConstraintName = constraintName;
            _context.Expressions.Add(expression);
            return new CreateConstraintExpressionBuilder(expression);
        }
        /// <inheritdoc />
        public ICreateConstraintOnTableSyntax PrimaryKey() {
            var expression = new CreateConstraintExpression(ConstraintType.PrimaryKey);
            _context.Expressions.Add(expression);
            return new CreateConstraintExpressionBuilder(expression);
        }
        /// <inheritdoc />
        public ICreateConstraintOnTableSyntax PrimaryKey(string primaryKeyName) {
            var expression = new CreateConstraintExpression(ConstraintType.PrimaryKey);
            expression.Constraint.ConstraintName = primaryKeyName;
            _context.Expressions.Add(expression);
            return new CreateConstraintExpressionBuilder(expression);
        }
    }
}