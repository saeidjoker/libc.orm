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

using libc.orm.DatabaseMigration.Abstractions.Builders.Schema.Schema;
using libc.orm.DatabaseMigration.Abstractions.Builders.Schema.Table;
namespace libc.orm.DatabaseMigration.Abstractions.Builders.Schema {
    /// <summary>
    ///     The expression root to query the existence of database objects
    /// </summary>
    public interface ISchemaExpressionRoot : IFluentSyntax {
        /// <summary>
        ///     Specify the table as base to query a database objects existence
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <returns>The next step</returns>
        ISchemaTableSyntax Table(string tableName);
        /// <summary>
        ///     Specify the schema as base to query a database objects existence
        /// </summary>
        /// <param name="schemaName">The schema name</param>
        /// <returns>The next step</returns>
        ISchemaSchemaSyntax Schema(string schemaName);
    }
}