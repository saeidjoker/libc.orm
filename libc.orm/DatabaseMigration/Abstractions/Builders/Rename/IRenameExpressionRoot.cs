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

using libc.orm.DatabaseMigration.Abstractions.Builders.Rename.Table;

namespace libc.orm.DatabaseMigration.Abstractions.Builders.Rename
{
    /// <summary>
    ///     The expression root for renaming tables or columns
    /// </summary>
    public interface IRenameExpressionRoot : IFluentSyntax
    {
        /// <summary>
        ///     Specify the table (or its column) to be renamed
        /// </summary>
        /// <param name="oldName">The current table name</param>
        /// <returns>The next step</returns>
        IRenameTableToOrInSchemaSyntax Table(string oldName);

        /// <summary>
        ///     Specify the column to be renamed
        /// </summary>
        /// <param name="oldName">The current column name</param>
        /// <returns>The next step</returns>
        IRenameColumnTableSyntax Column(string oldName);
    }
}