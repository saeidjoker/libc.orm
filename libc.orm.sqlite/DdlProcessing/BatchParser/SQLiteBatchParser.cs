#region License

// Copyright (c) 2018, Fluent Migrator Project
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

using System.Collections.Generic;
using libc.orm.DatabaseMigration.DdlProcessing.BatchParser;
using libc.orm.DatabaseMigration.DdlProcessing.BatchParser.RangeSearchers;
using libc.orm.DatabaseMigration.DdlProcessing.BatchParser.SpecialTokenSearchers;

namespace libc.orm.sqlite.DdlProcessing.BatchParser
{
    /// <summary>
    ///     A specialization of the <see cref="SqlBatchParser" /> for the Microsoft SQL Server
    /// </summary>
    /// <remarks>
    ///     It uses the following range searchers:
    ///     <see cref="MultiLineComment" />
    ///     ,
    ///     <see cref="DoubleDashSingleLineComment" />,
    ///     <see cref="PoundSignSingleLineComment" />,
    ///     <see cref="AnsiSqlIdentifier" />,
    ///     <see cref="System.Data.SqlTypes.SqlString" />
    ///     and the following token searchers: <see cref="GoSearcher" />.
    /// </remarks>
    public class SQLiteBatchParser : SqlBatchParser
    {
        private static readonly IEnumerable<IRangeSearcher> _rangeSearchers = new IRangeSearcher[]
        {
            new MultiLineComment(),
            new DoubleDashSingleLineComment(),
            new AnsiSqlIdentifier(),
            new SqlString()
        };

        private static readonly IEnumerable<ISpecialTokenSearcher> _specialTokenSearchers = new ISpecialTokenSearcher[]
        {
            new GoSearcher()
        };

        /// <summary>
        ///     Initializes a new instance of the <see cref="SQLiteBatchParser" /> class.
        /// </summary>
        /// <param name="newLine">The string used to write a new line sequence</param>
        public SQLiteBatchParser(string newLine = null)
            : base(_rangeSearchers, _specialTokenSearchers, newLine)
        {
        }
    }
}