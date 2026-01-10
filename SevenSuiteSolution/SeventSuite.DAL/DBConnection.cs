using System.Configuration;
using System.Data.SqlClient;

namespace SeventSuite.DAL
{
    public static class  DbConnection
    {
        public const string DEFAULT_CONNECTION = "DefaultConnection";
        public static SqlConnection Create()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[DEFAULT_CONNECTION].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
