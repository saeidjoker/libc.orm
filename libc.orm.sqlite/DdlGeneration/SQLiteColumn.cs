using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using libc.orm.DatabaseMigration.Abstractions.Model;
using libc.orm.DatabaseMigration.DdlGeneration;

namespace libc.orm.sqlite.DdlGeneration
{
    // ReSharper disable once InconsistentNaming
    internal class SQLiteColumn : ColumnBase
    {
        public SQLiteColumn()
            : base(new SQLiteTypeMap(), new SQLiteQuoter())
        {
        }

        /// <inheritdoc />
        public override string Generate(IEnumerable<ColumnDefinition> columns, string tableName)
        {
            var colDefs = columns.ToList();
            var foreignKeyColumns = colDefs.Where(x => x.IsForeignKey && x.ForeignKey != null);

            var foreignKeyClauses = foreignKeyColumns
                .Select(x => ", " + FormatForeignKey(x.ForeignKey, GenerateForeignKeyName));

            // Append foreign key definitions after all column definitions and the primary key definition
            return base.Generate(colDefs, tableName) + string.Concat(foreignKeyClauses);
        }

        /// <inheritdoc />
        protected override string FormatIdentity(ColumnDefinition column)
        {
            //SQLite only supports the concept of Identity in combination with a single primary key
            //see: http://www.sqlite.org/syntaxdiagrams.html#column-constraint syntax details
            if (column.IsIdentity && !column.IsPrimaryKey && column.Type != DbType.Int32)
                throw new ArgumentException("SQLite only supports identity on single integer, primary key coulmns");

            return string.Empty;
        }

        /// <inheritdoc />
        public override bool ShouldPrimaryKeysBeAddedSeparately(IEnumerable<ColumnDefinition> primaryKeyColumns)
        {
            //If there are no identity column then we can add as a separate constrint
            var pkColDefs = primaryKeyColumns.ToList();

            return !pkColDefs.Any(x => x.IsIdentity) && pkColDefs.Any(x => x.IsPrimaryKey);
        }

        /// <inheritdoc />
        protected override string FormatPrimaryKey(ColumnDefinition column)
        {
            if (!column.IsPrimaryKey) return string.Empty;

            return column.IsIdentity ? "PRIMARY KEY AUTOINCREMENT" : string.Empty;
        }
    }
}