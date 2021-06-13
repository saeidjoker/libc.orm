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

namespace libc.orm.DatabaseMigration.Abstractions.Builders.Create.ForeignKey
{
    /// <summary>
    ///     Interface to define the foreign key columns or the foreign keys table schema
    /// </summary>
    public interface ICreateForeignKeyForeignColumnOrInSchemaSyntax : ICreateForeignKeyForeignColumnSyntax
    {
        /// <summary>
        ///     Specify the schema of the foreign key table
        /// </summary>
        /// <param name="schemaName">The schema name</param>
        /// <returns>Specify the foreign key columns</returns>
        ICreateForeignKeyForeignColumnSyntax InSchema(string schemaName);
    }
}