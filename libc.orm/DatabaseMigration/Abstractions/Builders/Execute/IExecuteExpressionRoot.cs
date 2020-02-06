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
using System.Data;
namespace libc.orm.DatabaseMigration.Abstractions.Builders.Execute {
    /// <summary>
    ///     Executes some SQL
    /// </summary>
    public interface IExecuteExpressionRoot : IFluentSyntax {
        /// <summary>
        ///     Executes an SQL statement
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute</param>
        void Sql(string sqlStatement);
        /// <summary>
        ///     Calls an action to execute dynamically generated SQL statements
        /// </summary>
        /// <param name="operation">The operation to execute on a given connection and transaction</param>
        void WithConnection(Action<IDbConnection, IDbTransaction> operation);
    }
}