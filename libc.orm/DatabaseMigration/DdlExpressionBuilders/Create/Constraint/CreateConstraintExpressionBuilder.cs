using System.Collections.Generic;
using libc.orm.DatabaseMigration.Abstractions;
using libc.orm.DatabaseMigration.Abstractions.Builders.Create.Constraint;
using libc.orm.DatabaseMigration.Abstractions.Expressions;
namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.Create.Constraint {
    /// <summary>
    ///     An expression builder for a <see cref="CreateConstraintExpression" />
    /// </summary>
    public class CreateConstraintExpressionBuilder : ExpressionBuilderBase<CreateConstraintExpression>,
        ICreateConstraintOnTableSyntax,
        ICreateConstraintWithSchemaOrColumnSyntax,
        ICreateConstraintOptionsSyntax,
        ISupportAdditionalFeatures {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:CreateConstraintExpressionBuilder" /> class.
        /// </summary>
        /// <param name="expression">The underlying expression</param>
        public CreateConstraintExpressionBuilder(CreateConstraintExpression expression)
            : base(expression) {
        }
        /// <inheritdoc />
        public ICreateConstraintWithSchemaOrColumnSyntax OnTable(string tableName) {
            Expression.Constraint.TableName = tableName;
            return this;
        }
        /// <inheritdoc />
        public ICreateConstraintOptionsSyntax Column(string columnName) {
            Expression.Constraint.Columns.Add(columnName);
            return this;
        }
        /// <inheritdoc />
        public ICreateConstraintOptionsSyntax Columns(params string[] columnNames) {
            foreach (var columnName in columnNames) Expression.Constraint.Columns.Add(columnName);
            return this;
        }
        /// <inheritdoc />
        public ICreateConstraintColumnsSyntax WithSchema(string schemaName) {
            Expression.Constraint.SchemaName = schemaName;
            return this;
        }
        /// <inheritdoc />
        public IDictionary<string, object> AdditionalFeatures => Expression.Constraint.AdditionalFeatures;
    }
}