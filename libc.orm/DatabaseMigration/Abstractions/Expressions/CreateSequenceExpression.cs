#region License

// Copyright (c) 2007-2009, Fluent Migrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Collections.Generic;
using libc.orm.DatabaseMigration.Abstractions.Expressions.Base;
using libc.orm.DatabaseMigration.Abstractions.Model;
using libc.orm.DatabaseMigration.Abstractions.Validation;
using libc.orm.DatabaseMigration.DdlProcessing;
namespace libc.orm.DatabaseMigration.Abstractions.Expressions {
    /// <summary>
    ///     Expression to crate a sequence
    /// </summary>
    public class CreateSequenceExpression : MigrationExpressionBase, ISequenceExpression, IValidationChildren {
        /// <inheritdoc />
        public virtual SequenceDefinition Sequence { get; set; } = new SequenceDefinition();
        /// <inheritdoc />
        public IEnumerable<object> Children {
            get {
                yield return Sequence;
            }
        }
        public override void ExecuteWith(IProcessor processor) {
            processor.Process(this);
        }
        /// <inheritdoc />
        public override string ToString() {
            return base.ToString() + Sequence.Name;
        }
    }
}