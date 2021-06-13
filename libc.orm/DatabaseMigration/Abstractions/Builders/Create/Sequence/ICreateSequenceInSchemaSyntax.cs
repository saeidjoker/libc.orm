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

namespace libc.orm.DatabaseMigration.Abstractions.Builders.Create.Sequence
{
    /// <summary>
    ///     Defines a sequence and (optionally) the schema it's stored in
    /// </summary>
    public interface ICreateSequenceInSchemaSyntax : ICreateSequenceSyntax
    {
        /// <summary>
        ///     Defines the schema of the sequence
        /// </summary>
        /// <param name="schemaName">The schema name</param>
        /// <returns>Defines the sequence options</returns>
        ICreateSequenceSyntax InSchema(string schemaName);
    }
}