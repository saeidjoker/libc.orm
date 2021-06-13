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

namespace libc.orm.DatabaseMigration.Abstractions.Builders.Update
{
    /// <summary>
    ///     Interface the specify the update condition
    /// </summary>
    public interface IUpdateWhereSyntax : IFluentSyntax
    {
        /// <summary>
        ///     Specify the condition of the rows to update
        /// </summary>
        /// <param name="dataAsAnonymousType">The columns and values to be used as condition</param>
        void Where(object dataAsAnonymousType);

        /// <summary>
        ///     Specify that all rows should be updated
        /// </summary>
        void AllRows();
    }
}