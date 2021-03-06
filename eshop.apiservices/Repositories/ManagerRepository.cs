﻿using Dapper;
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
    public class ManagerRepository : BaseRepositoty, IManagerRepository
    {
        public ManagerRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<ManagerViewModel>> GetAllManagersAsync()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "MNG_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = "ESHOP_MANAGER_API.SP_MANAGER_GETALLMANAGER";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ManagerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RoleViewModel>> GetAllRoleAsync()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "MNG_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = "ESHOP_MANAGER_API.SP_MANAGER_GETALLROLE";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<RoleViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ManagerViewModel> GetManagerByIdAsync(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "MNG_ID", value: id, dbType: OracleMappingType.Int32, direction: ParameterDirection.Input);
                param.Add(name: "MNG_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = "ESHOP_MANAGER_API.SP_MANAGER_GETMANAGERBYID";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ManagerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<ManagerViewModel> GetManagerByEmailAsync(string email)
        //{
        //    try
        //    {
        //        var param = new OracleDynamicParameters();
        //        param.Add(name: "MNG_EMAIL", value: email, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
        //        param.Add(name: "MNG_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

        //        var query = "ESHOP_MANAGER_API.SP_MANAGER_GETMANAGERBYEMAIL";
        //        var conn = GetOpenConnection();

        //        var result = (await SqlMapper.QueryAsync<ManagerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<ManagerViewModel> AuthenticateManagerAsync(string email, string passwordHash)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "MNG_EMAIL", value: email, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "MNG_PASSWORD_HASH", value: passwordHash, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "MNG_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = "ESHOP_MANAGER_API.SP_MANAGER_AUTHENTICATEMANAGER";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ManagerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ManagerViewModel> AddManagerAsync(Manager manager)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "MNG_ROLE_ID", value: manager.Role_Id, dbType: OracleMappingType.Int32, direction: ParameterDirection.Input);
                param.Add(name: "MNG_FIRSTNAME", value: manager.FirstName, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "MNG_LASTNAME", value: manager.LastName, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "MNG_EMAIL", value: manager.Email, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "MNG_PASSWORD_HASH", value: manager.Password_Hash, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "MNG_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = "ESHOP_MANAGER_API.SP_MANAGER_ADDMANAGER";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ManagerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ManagerViewModel> UpdateManagerAsync(int id, Manager manager)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "MNG_ID", value: id, dbType: OracleMappingType.Int32, direction: ParameterDirection.Input);
                param.Add(name: "MNG_ROLE_ID", value: manager.Role_Id, dbType: OracleMappingType.Int32, direction: ParameterDirection.Input);
                param.Add(name: "MNG_FIRSTNAME", value: manager.FirstName, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "MNG_LASTNAME", value: manager.LastName, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "MNG_EMAIL", value: manager.Email, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "MNG_PASSWORD_HASH", value: manager.Password_Hash, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "MNG_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = "ESHOP_MANAGER_API.SP_MANAGER_UPDATEMANAGER";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ManagerViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteManagerAsync(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "MNG_ID", value: id, dbType: OracleMappingType.Int32, direction: ParameterDirection.Input);

                var query = "ESHOP_MANAGER_API.SP_MANAGER_DELETEMANAGER";
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
