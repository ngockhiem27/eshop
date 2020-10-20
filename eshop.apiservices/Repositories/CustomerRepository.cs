using Dapper;
using Dapper.Oracle;
using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using eshop.core.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.apiservices.Repositories
{
    public class CustomerRepository : BaseRepositoty, ICustomerRepository
    {
        private const string CUSTOMER_STORED_PACKAGE = "ESHOP_CUSTOMER_API";
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<CustomerViewModel>> GetAllCustomersAsync()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CTM_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = CUSTOMER_STORED_PACKAGE + ".SP_CUSTOMER_GETALLCUSTOMER";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<CustomerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerViewModel> GetCustomerByEmailAsync(string email)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CTM_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add(name: "CTM_EMAIL", email, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);

                var query = CUSTOMER_STORED_PACKAGE + ".SP_CUSTOMER_GETCUSTOMERBYEMAIL";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<CustomerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerViewModel> GetCustomerByIdAsync(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CTM_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add(name: "CTM_ID", id, dbType: OracleMappingType.Int32, direction: ParameterDirection.Input);

                var query = CUSTOMER_STORED_PACKAGE + ".SP_CUSTOMER_GETCUSTOMERBYID";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<CustomerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerViewModel> AuthenticateCustomerAsync(string email, string passwordHash)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CTM_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add(name: "CTM_EMAIL", email, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "CTM_PASSWORD_HASH", passwordHash, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);

                var query = CUSTOMER_STORED_PACKAGE + ".SP_CUSTOMER_AUTHENTICATECUSTOMER";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<CustomerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerViewModel> AddCustomerAsync(Customer customer)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CTM_FIRSTNAME", value: customer.FirstName, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "CTM_LASTNAME", value: customer.LastName, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "CTM_EMAIL", value: customer.Email, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "CTM_PASSWORD_HASH", value: customer.Password_Hash, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "CTM_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = CUSTOMER_STORED_PACKAGE + ".SP_CUSTOMER_ADDCUSTOMER";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<CustomerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerViewModel> UpdateCustomerAsync(int id, Customer customer)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CTM_ID", value: id, dbType: OracleMappingType.Int32, direction: ParameterDirection.Input);
                param.Add(name: "CTM_FIRSTNAME", value: customer.FirstName, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "CTM_LASTNAME", value: customer.LastName, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "CTM_EMAIL", value: customer.Email, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "CTM_PASSWORD_HASH", value: customer.Password_Hash, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "CTM_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = CUSTOMER_STORED_PACKAGE + ".SP_CUSTOMER_UPDATECUSTOMER";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<CustomerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteCustomerAsync(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CTM_ID", value: id, dbType: OracleMappingType.Int32, direction: ParameterDirection.Input);

                var query = CUSTOMER_STORED_PACKAGE + ".SP_CUSTOMER_DELETECUSTOMER";
                var conn = GetOpenConnection();

                int rowEffected = await SqlMapper.ExecuteAsync(conn, query, param: param, commandType: CommandType.StoredProcedure);
                return rowEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
