using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;

namespace eshop.apiservices.Repositories
{
    public abstract class BaseRepositoty : IDisposable
    {
        private readonly IConfiguration configuration;
        private DbConnection _dbConn;

        public BaseRepositoty(IConfiguration configuration)
        {
            this.configuration = configuration;
            _dbConn = OracleClientFactory.Instance.CreateConnection();
            _dbConn.ConnectionString = configuration.GetConnectionString("Oracle");
        }

        public IDbConnection GetOpenConnection()
        {
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
