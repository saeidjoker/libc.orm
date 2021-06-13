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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using libc.orm.DatabaseMigration.Abstractions.Expressions.Base;
using libc.orm.DatabaseMigration.Abstractions.Model;
using libc.orm.DatabaseMigration.DdlProcessing;
using libc.orm.Resources;

namespace libc.orm.DatabaseMigration.Abstractions.Expressions
{
    /// <summary>
    ///     Expression to delete an index
    /// </summary>
    public class DeleteIndexExpression : MigrationExpressionBase, ISupportAdditionalFeatures, IIndexExpression,
        IValidatableObject
    {
        /// <inheritdoc />
        public virtual IndexDefinition Index { get; set; } = new IndexDefinition();

        /// <inheritdoc />
        public IDictionary<string, object> AdditionalFeatures => Index.AdditionalFeatures;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Index.Name)) yield return new ValidationResult(Dmt.IndexNameCannotBeNullOrEmpty);

            if (string.IsNullOrEmpty(Index.TableName))
                yield return new ValidationResult(Dmt.TableNameCannotBeNullOrEmpty);
        }

        public override void ExecuteWith(IProcessor processor)
        {
            processor.Process(this);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return base.ToString() + Index.TableName + " (" +
                   string.Join(", ", Index.Columns.Select(x => x.Name).ToArray()) + ")";
        }
    }
}