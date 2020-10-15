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
    public class CategoryRepository : BaseRepositoty, ICategoryRepository
    {
        private const string PRODUCT_STORED_PACKAGE = "ESHOP_PRODUCT_API";

        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CAT_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETALLCATEGORY";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<CategoryViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductCategoryViewModel>> GetAllCategoryWithProductAsync()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CAT_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETALLCATEGORYLEFTJOINPRODUCT";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ProductCategoryViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoryViewModel> GetCategoryAsync(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CAT_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add(name: "CAT_ID", id, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETCATEGORYBYID";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<CategoryViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoryViewModel> AddCategoryAsync(Category category)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CAT_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add(name: "CAT_NAME", category.Name, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_ADDCATEGORY";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<CategoryViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoryViewModel> UpdateCategoryAsync(int id, Category category)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CAT_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add(name: "CAT_ID", id, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);
                param.Add(name: "CAT_NAME", category.Name, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_UPDATECATEGORY";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<CategoryViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "CAT_ID", id, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_DELETECATEGORY";
                var conn = GetOpenConnection();

                var rowEffected = await SqlMapper.ExecuteAsync(conn, query, param: param, commandType: CommandType.StoredProcedure);

                return rowEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
