namespace Rnsx.Stockify.Data
{
    class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string GetConnectionString()
        {
            return ApplicationConfiguration.ConnectionString;
        }
    }
}