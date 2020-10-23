using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace eshop.apiservices.Repositories
{
    public abstract class BaseRepositoty : IDisposable
    {
        private readonly IConfiguration configuration;

        public BaseRepositoty(IConfiguration configuration)
        {
            this.configuration = configuration;

        }

        public IDbConnection GetOpenConnection()
        {
            var _dbConn = OracleClientFactory.Instance.CreateConnection();
            _dbConn.ConnectionString = configuration.GetConnectionString("Oracle");
            if (_dbConn.State == ConnectionState.Closed)
            {
                _dbConn.Open();
            }
            return _dbConn;
        }

        public void Dispose()
        {
            // _dbConn.Dispose();
        }
    }
}
