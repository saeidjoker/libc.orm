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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using libc.orm.DatabaseMigration.Abstractions.Builders.Update;
using libc.orm.DatabaseMigration.Abstractions.Expressions;
using libc.orm.DatabaseMigration.DdlMigration;
namespace libc.orm.DatabaseMigration.DdlExpressionBuilders.Update {
    /// <summary>
    ///     An expression builder for a <see cref="UpdateDataExpression" />
    /// </summary>
    public class UpdateDataExpressionBuilder : IUpdateSetOrInSchemaSyntax,
        IUpdateWhereSyntax {
        private readonly UpdateDataExpression _expression;
        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdateDataExpressionBuilder" /> class.
        /// </summary>
        /// <param name="expression">The underlying expression</param>
        public UpdateDataExpressionBuilder(UpdateDataExpression expression) {
            _expression = expression;
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdateDataExpressionBuilder" /> class.
        /// </summary>
        /// <param name="expression">The underlying expression</param>
        /// <param name="context">The migration context</param>
        [Obsolete]
        // ReSharper disable once UnusedParameter.Local
        public UpdateDataExpressionBuilder(UpdateDataExpression expression, MigrationContext context) {
            _expression = expression;
        }
        /// <inheritdoc />
        public IUpdateSetSyntax InSchema(string schemaName) {
            _expression.SchemaName = schemaName;
            return this;
        }
        /// <inheritdoc />
        public IUpdateWhereSyntax Set(object dataAsAnonymousType) {
            _expression.Set = GetData(dataAsAnonymousType);
            return this;
        }
        /// <inheritdoc />
        public void Where(object dataAsAnonymousType) {
            _expression.Where = GetData(dataAsAnonymousType);
        }
        /// <inheritdoc />
        public void AllRows() {
            _expression.IsAllRows = true;
        }
        private static List<KeyValuePair<string, object>> GetData(object dataAsAnonymousType) {
            var data = new List<KeyValuePair<string, object>>();
            var properties = TypeDescriptor.GetProperties(dataAsAnonymousType);
            foreach (PropertyDescriptor property in properties)
                data.Add(new KeyValuePair<string, object>(property.Name, property.GetValue(dataAsAnonymousType)));
            return data;
        }
    }
}