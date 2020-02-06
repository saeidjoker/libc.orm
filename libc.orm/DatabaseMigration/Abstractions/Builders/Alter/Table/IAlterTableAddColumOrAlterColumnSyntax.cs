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

namespace libc.orm.DatabaseMigration.Abstractions.Builders.Alter.Table {
    /// <summary>
    ///     Interface to add or alter a column
    /// </summary>
    public interface IAlterTableAddColumnOrAlterColumnSyntax : IFluentSyntax {
        /// <summary>
        ///     Add a column
        /// </summary>
        /// <param name="name">The column name</param>
        /// <returns>The interface to define the column properties</returns>
        IAlterTableColumnAsTypeSyntax AddColumn(string name);
        /// <summary>
        ///     Alter a column
        /// </summary>
        /// <param name="name">The column name</param>
        /// <returns>The interface to define the column properties</returns>
        IAlterTableColumnAsTypeSyntax AlterColumn(string name);
        /// <summary>
        ///     Set the table schema
        /// </summary>
        /// <param name="name">The schema name</param>
        void ToSchema(string name);
    }
}