namespace Core.CrossCuttingConcerns.Serilog.ConfigurationModels
{
    public class MssqlConfiguration
    {
        public string ConnectionString { get; set; }

        public string Table { get; set; }

        public bool AutoCreateSqlTable { get; set; }

        public MssqlConfiguration()
        {
            ConnectionString = string.Empty;
            Table = string.Empty;
        }

        public MssqlConfiguration(string connectionString, string tableName, bool autoCreateTable)
        {
            ConnectionString = connectionString;
            Table = tableName;
            AutoCreateSqlTable = autoCreateTable;
        }
    }
}
