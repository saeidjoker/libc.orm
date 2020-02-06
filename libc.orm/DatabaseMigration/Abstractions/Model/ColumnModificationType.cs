namespace libc.orm.DatabaseMigration.Abstractions.Model {
    /// <summary>
    ///     Indicates wheter a column should be created or altered
    /// </summary>
    public enum ColumnModificationType {
        /// <summary>
        ///     The column in question should be created
        /// </summary>
        Create,
        /// <summary>
        ///     The column in question should be altered
        /// </summary>
        Alter
    }
}