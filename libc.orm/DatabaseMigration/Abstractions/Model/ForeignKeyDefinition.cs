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
using System.ComponentModel.DataAnnotations;
using System.Data;
using libc.orm.Resources;

namespace libc.orm.DatabaseMigration.Abstractions.Model
{
    /// <summary>
    ///     The foreign key definition
    /// </summary>
    public class ForeignKeyDefinition : ICloneable, IValidatableObject
    {
        /// <summary>
        ///     Gets or sets a foreign key name
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Dmt),
            ErrorMessageResourceName = "ForeignKeyNameCannotBeNullOrEmpty")]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Gets or sets the foreign key table
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Dmt),
            ErrorMessageResourceName = "ForeignTableNameCannotBeNullOrEmpty")]
        public virtual string ForeignTable { get; set; }

        /// <summary>
        ///     Gets or sets the foreign keys table schema
        /// </summary>
        public virtual string ForeignTableSchema { get; set; }

        /// <summary>
        ///     Gets or sets the primary table
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Dmt),
            ErrorMessageResourceName = "PrimaryTableNameCannotBeNullOrEmpty")]
        public virtual string PrimaryTable { get; set; }

        /// <summary>
        ///     Gets or sets the primary table schema
        /// </summary>
        public virtual string PrimaryTableSchema { get; set; }

        /// <summary>
        ///     Gets or sets the rule for a cascading DELETE
        /// </summary>
        public virtual Rule OnDelete { get; set; }

        /// <summary>
        ///     Gets or sets the rule for a cascading UPDATE
        /// </summary>
        public virtual Rule OnUpdate { get; set; }

        /// <summary>
        ///     GEts or sets the foreign key column names
        /// </summary>
        public virtual ICollection<string> ForeignColumns { get; set; } = new List<string>();

        /// <summary>
        ///     Gets or sets the primary key column names
        /// </summary>
        public virtual ICollection<string> PrimaryColumns { get; set; } = new List<string>();

        /// <inheritdoc />
        public object Clone()
        {
            return new ForeignKeyDefinition
            {
                Name = Name,
                ForeignTableSchema = ForeignTableSchema,
                ForeignTable = ForeignTable,
                PrimaryTableSchema = PrimaryTableSchema,
                PrimaryTable = PrimaryTable,
                ForeignColumns = new List<string>(ForeignColumns),
                PrimaryColumns = new List<string>(PrimaryColumns),
                OnDelete = OnDelete,
                OnUpdate = OnUpdate
            };
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ForeignColumns.Count == 0)
                yield return new ValidationResult(Dmt.ForeignKeyMustHaveOneOrMoreForeignColumns);

            if (PrimaryColumns.Count == 0)
                yield return new ValidationResult(Dmt.ForeignKeyMustHaveOneOrMorePrimaryColumns);
        }

        /// <summary>
        ///     Gets a value indicating whether primary and foreign key columns are defined
        /// </summary>
        /// <returns></returns>
        public bool HasForeignAndPrimaryColumnsDefined()
        {
            return ForeignColumns.Count > 0 && PrimaryColumns.Count > 0;
        }
    }
}