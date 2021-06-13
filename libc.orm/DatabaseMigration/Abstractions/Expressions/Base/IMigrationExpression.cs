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

using libc.orm.DatabaseMigration.DdlProcessing;

namespace libc.orm.DatabaseMigration.Abstractions.Expressions.Base
{
    /// <summary>
    ///     The interface for a migration expression
    /// </summary>
    public interface IMigrationExpression
    {
        /// <summary>
        ///     Execute the expression with the given <paramref name="processor" />
        /// </summary>
        /// <param name="processor">The processor to execute the expression with</param>
        void ExecuteWith(IProcessor processor);

        /// <summary>
        ///     Create a reversing migration expression
        /// </summary>
        /// <returns>The reversing migration expression</returns>
        IMigrationExpression Reverse();
    }
}