#region License

// Copyright (c) 2007-2018, FluentMigrator Project
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

namespace libc.orm.DatabaseMigration.Abstractions.Builders.Delete.Constraint {
    /// <summary>
    ///     Define the schema of the constraint to delete
    /// </summary>
    public interface IDeleteConstraintInSchemaOptionsSyntax : IDeleteConstraintColumnSyntax {
        /// <summary>
        ///     Define the schema of the constraint to delete
        /// </summary>
        /// <param name="schemaName">The schema name</param>
        /// <returns>The next step</returns>
        IDeleteConstraintInSchemaOptionsSyntax InSchema(string schemaName);
    }
}