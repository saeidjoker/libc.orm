using System.Collections.Generic;
using libc.orm.DatabaseMigration.Abstractions.Builders;
using libc.orm.DatabaseMigration.Abstractions.Expressions;
using libc.orm.DatabaseMigration.Abstractions.Model;
using libc.orm.DatabaseMigration.DdlMigration;

#region License

//
// Copyright (c) 2018, Fluent Migrator Project
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

namespace libc.orm.DatabaseMigration.DdlExpressionBuilders {
    /// <summary>
    ///     This class provides a common location for logic pertaining to setting and maintaining
    ///     expressions for expression builders which manipulate the the ColumnDefinition.
    /// </summary>
    /// <remarks>
    ///     This is a support class for the migrator framework and is not intended for external use.
    ///     TODO: make this internal, and the change assmebly info so InternalsVisibleTo is set for the test assemblies.
    /// </remarks>
    public class ColumnExpressionBuilderHelper {
        private readonly IColumnExpressionBuilder _builder;
        private readonly MigrationContext _context;
        private readonly Dictionary<ColumnDefinition, ExistingRowsData> _existingRowsDataByColumn;
        /// <summary>
        ///     Initializes a new instance of the <see cref="ColumnExpressionBuilderHelper" /> class.
        /// </summary>
        /// <remarks>
        ///     This constructor exists only to ease creating mock objects.
        /// </remarks>
        protected ColumnExpressionBuilderHelper() {
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="ColumnExpressionBuilderHelper" /> class.
        /// </summary>
        /// <param name="builder">The expression builder</param>
        /// <param name="context">The migration context</param>
        public ColumnExpressionBuilderHelper(IColumnExpressionBuilder builder, MigrationContext context) {
            _builder = builder;
            _context = context;
            _existingRowsDataByColumn = new Dictionary<ColumnDefinition, ExistingRowsData>();
        }
        /// <summary>
        ///     Either updates the IsNullable flag on the column, or creates/removes the SetNotNull expression, depending
        ///     on whether the column has a 'Set existing rows' expression.
        /// </summary>
        public virtual void SetNullable(bool isNullable) {
            var column = _builder.Column;
            ExistingRowsData exRowExpr;
            if (_existingRowsDataByColumn.TryGetValue(column, out exRowExpr))
                if (exRowExpr.SetExistingRowsExpression != null) {
                    if (isNullable) {
                        //Remove additional expression to set column to not null.
                        _context.Expressions.Remove(exRowExpr.SetColumnNotNullableExpression);
                        exRowExpr.SetColumnNotNullableExpression = null;
                    } else {
                        //Add expression to set column to not null.
                        //If it already exists, just leave it.
                        if (exRowExpr.SetColumnNotNullableExpression == null) {
                            //stuff that matters shouldn't change at this point, so we're free to make a
                            //copy of the col def.
                            //TODO: make a SetColumnNotNullExpression, which just takes the bare minimum, rather
                            //than modifying column with all parameters.
                            var notNullColDef = (ColumnDefinition) column.Clone();
                            notNullColDef.ModificationType = ColumnModificationType.Alter;
                            notNullColDef.IsNullable = false;
                            exRowExpr.SetColumnNotNullableExpression = new AlterColumnExpression {
                                Column = notNullColDef,
                                TableName = _builder.TableName,
                                SchemaName = _builder.SchemaName
                            };
                            _context.Expressions.Add(exRowExpr.SetColumnNotNullableExpression);
                        }
                    }

                    //Setting column explicitly to nullable, as the actual nullable value
                    //will be set by the SetColumnNotNullableExpression after the column is created and populated.
                    column.IsNullable = true;
                    return;
                }

            //At this point, we know there's no existing row expression, so just pass it onto the
            //underlying column.
            column.IsNullable = isNullable;
        }
        /// <summary>
        ///     Adds the existing row default value.  If the column has a value for IsNullable, this will also
        ///     call SetNullable to create the expression, and will then set the column IsNullable to false.
        /// </summary>
        public virtual void SetExistingRowsTo(object existingRowValue) {
            //TODO: validate that 'value' isn't set to null for non nullable columns.  If set to
            //null, maybe just remove the expressions?.. not sure of best way to handle this.
            var column = _builder.Column;
            if (column.ModificationType == ColumnModificationType.Create) {
                //ensure an UpdateDataExpression is created and cached for this column
                ExistingRowsData exRowExpr;
                if (!_existingRowsDataByColumn.TryGetValue(column, out exRowExpr)) {
                    exRowExpr = new ExistingRowsData();
                    _existingRowsDataByColumn.Add(column, exRowExpr);
                }
                if (exRowExpr.SetExistingRowsExpression == null) {
                    exRowExpr.SetExistingRowsExpression = new UpdateDataExpression {
                        TableName = _builder.TableName,
                        SchemaName = _builder.SchemaName,
                        IsAllRows = true
                    };
                    _context.Expressions.Add(exRowExpr.SetExistingRowsExpression);

                    //Call SetNullable, to ensure that not-null columns are correctly set to
                    //not null after existing rows have data populated.
                    SetNullable(column.IsNullable ?? true);
                }
                exRowExpr.SetExistingRowsExpression.Set = new List<KeyValuePair<string, object>> {
                    new KeyValuePair<string, object>(column.Name, existingRowValue)
                };
            }
        }
        /// <summary>
        ///     Creates a new CREATE INDEX expression to create a new unique index
        /// </summary>
        /// <param name="indexName">The new name of the index</param>
        public virtual void Unique(string indexName) {
            var column = _builder.Column;
            column.IsUnique = true;
            var index = new CreateIndexExpression {
                Index = new IndexDefinition {
                    Name = indexName,
                    SchemaName = _builder.SchemaName,
                    TableName = _builder.TableName,
                    IsUnique = true
                }
            };
            index.Index.Columns.Add(new IndexColumnDefinition {
                Name = _builder.Column.Name
            });
            _context.Expressions.Add(index);
        }
        /// <summary>
        ///     Creates a new CREATE INDEX expression
        /// </summary>
        /// <param name="indexName">The index name</param>
        public virtual void Indexed(string indexName) {
            _builder.Column.IsIndexed = true;
            var index = new CreateIndexExpression {
                Index = new IndexDefinition {
                    Name = indexName,
                    SchemaName = _builder.SchemaName,
                    TableName = _builder.TableName
                }
            };
            index.Index.Columns.Add(new IndexColumnDefinition {
                Name = _builder.Column.Name
            });
            _context.Expressions.Add(index);
        }
        /// <summary>
        ///     For each distinct column which has an existing row default, an instance of this
        ///     will be stored in the _expressionsByColumn.
        /// </summary>
        private class ExistingRowsData {
            public AlterColumnExpression SetColumnNotNullableExpression;
            public UpdateDataExpression SetExistingRowsExpression;
        }
    }
}