using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Threading.Tasks;

namespace eshop.apiservices.Repositories
{
    public abstract class BaseRepositoty
    {
        private readonly IConfiguration configuration;

        public BaseRepositoty(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection GetOpenConnection()
        {
            var conn = new OracleConnection(configuration.GetConnectionString("Oracle"));
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        //public async Task<object> ExecuteQueryAsync()
        //{

        //}
    }
}
