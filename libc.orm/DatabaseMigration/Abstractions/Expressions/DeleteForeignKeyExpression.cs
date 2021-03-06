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
using libc.orm.DatabaseMigration.Abstractions.Validation;
using libc.orm.DatabaseMigration.DdlProcessing;
using libc.orm.Resources;

namespace libc.orm.DatabaseMigration.Abstractions.Expressions
{
    /// <summary>
    ///     Expression to delete a foreign key
    /// </summary>
    public class DeleteForeignKeyExpression : MigrationExpressionBase, IForeignKeyExpression, IValidatableObject
    {
        /// <inheritdoc />
        public virtual ForeignKeyDefinition ForeignKey { get; set; } = new ForeignKeyDefinition();

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (ForeignKey.ForeignColumns.Count > 0)
            {
                var ctxt = new ValidationContext(ForeignKey, validationContext.Items);
                ctxt.InitializeServiceProvider(validationContext.GetService);
                ValidationUtilities.TryCollectResults(ctxt, ForeignKey, results);
            }
            else
            {
                if (string.IsNullOrEmpty(ForeignKey.Name))
                    results.Add(new ValidationResult(Dmt.ForeignKeyNameCannotBeNullOrEmpty));

                if (string.IsNullOrEmpty(ForeignKey.ForeignTable))
                    results.Add(new ValidationResult(Dmt.ForeignTableNameCannotBeNullOrEmpty));
            }

            return results;
        }

        public override void ExecuteWith(IProcessor processor)
        {
            processor.Process(this);
        }

        /// <inheritdoc />
        public override IMigrationExpression Reverse()
        {
            // there are 2 types of delete FK statements
            //  1) Delete.ForeignKey("FK_Name").OnTable("Table")
            //  2) Delete.ForeignKey()
            //      .FromTable("Table1").ForeignColumn("Id")
            //      .ToTable("Table2").PrimaryColumn("Id");

            // there isn't a way to autoreverse the type 1
            //  but we can turn the type 2 into Create.ForeignKey().FromTable() ...

            // only type 2 has foreign column(s) and primary column(s)
            if (!ForeignKey.HasForeignAndPrimaryColumnsDefined()) return base.Reverse();

            var reverseForeignKey = new ForeignKeyDefinition
            {
                Name = ForeignKey.Name,
                ForeignTableSchema = ForeignKey.PrimaryTableSchema,
                ForeignTable = ForeignKey.PrimaryTable,
                PrimaryTableSchema = ForeignKey.ForeignTableSchema,
                PrimaryTable = ForeignKey.ForeignTable,
                ForeignColumns = new List<string>(ForeignKey.PrimaryColumns),
                PrimaryColumns = new List<string>(ForeignKey.ForeignColumns),
                OnDelete = ForeignKey.OnDelete,
                OnUpdate = ForeignKey.OnUpdate
            };

            return new CreateForeignKeyExpression
            {
                ForeignKey = reverseForeignKey
            };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return base.ToString() + ForeignKey.Name + " "
                   + ForeignKey.ForeignTable + " (" + string.Join(", ", ForeignKey.ForeignColumns.ToArray()) + ") "
                   + ForeignKey.PrimaryTable + " (" + string.Join(", ", ForeignKey.PrimaryColumns.ToArray()) + ")";
        }
    }
}