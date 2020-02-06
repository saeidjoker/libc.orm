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

using System.ComponentModel.DataAnnotations;
using libc.orm.DatabaseMigration.Abstractions.Expressions.Base;
using libc.orm.DatabaseMigration.DdlProcessing;
using libc.orm.Resources;
namespace libc.orm.DatabaseMigration.Abstractions.Expressions {
    /// <summary>
    ///     Expression to execute an SQL statement
    /// </summary>
    public class ExecuteSqlStatementExpression : MigrationExpressionBase {
        /// <summary>
        ///     Gets or sets the SQL statement to be executed
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Dmt),
            ErrorMessageResourceName = nameof(Dmt.SqlStatementCannotBeNullOrEmpty))]
        public virtual string SqlStatement { get; set; }
        public override void ExecuteWith(IProcessor processor) {
            processor.Execute(SqlStatement);
        }
        /// <inheritdoc />
        public override string ToString() {
            return base.ToString() + SqlStatement;
        }
    }
}