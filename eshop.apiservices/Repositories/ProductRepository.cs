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
    public class ProductRepository : BaseRepositoty, IProductRepository
    {
        private const string PRODUCT_STORED_PACKAGE = "ESHOP_PRODUCT_API";

        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<ProductViewModel>> GetAllProductAsync()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETALLPRODUCT";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ProductViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductCategoryViewModel>> GetAllProductWithCategoryAsync()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETALLPRODUCTLEFTJOINCATEGORY";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ProductCategoryViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductViewModel> GetProductAsync(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add(name: "PRD_ID", id, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETPRODUCTBYID";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ProductViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<int>> GetProductCategoryAsync(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add(name: "PRD_ID", id, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETPRODUCTCATEGORYID";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<int>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductViewModel> AddProductAsync(Product product, List<CategoryViewModel> categories)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add(name: "PRD_NAME", product.Name, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "PRD_REGULAR_PRICE", product.Regular_Price, dbType: OracleMappingType.Decimal, direction: ParameterDirection.Input);
                param.Add(name: "PRD_DISCOUNT_PRICE", product.Discount_Price, dbType: OracleMappingType.Decimal, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_ADDPRODUCT";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ProductViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                for (int i = 0; i < categories.Count; i++)
                {
                    var rowEffected = await AddProductCategoryAsync(result.Id, categories[i].Id);
                }

                result.Categories = categories;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductViewModel> UpdateProductAsync(int id, Product product, List<CategoryViewModel> categories)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add(name: "PRD_ID", id, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);
                param.Add(name: "PRD_NAME", product.Name, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "PRD_REGULAR_PRICE", product.Regular_Price, dbType: OracleMappingType.Decimal, direction: ParameterDirection.Input);
                param.Add(name: "PRD_DISCOUNT_PRICE", product.Discount_Price, dbType: OracleMappingType.Decimal, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_UPDATEPRODUCT";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<ProductViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                var currentCategoryIds = await GetProductCategoryAsync(id);

                var catIdNeedDelete = currentCategoryIds.Where(id => !categories.Select(c => c.Id).Contains(id)).ToList();
                var catIdNeedInsert = categories.Select(c => c.Id).Where(id => !currentCategoryIds.Contains(id)).ToList();

                for (int i = 0; i < catIdNeedDelete.Count(); i++)
                {
                    await DeleteProductCategoryAsync(id, catIdNeedDelete[i]);
                }

                for (int i = 0; i < catIdNeedInsert.Count(); i++)
                {
                    await AddProductCategoryAsync(id, catIdNeedInsert[i]);
                }

                result.Categories = categories;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_ID", id, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_DELETEPRODUCT";
                var conn = GetOpenConnection();

                var rowEffected = await SqlMapper.ExecuteAsync(conn, query, param: param, commandType: CommandType.StoredProcedure);

                return rowEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddProductCategoryAsync(int productId, int categoryId)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_ID", productId, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);
                param.Add(name: "CAT_ID", categoryId, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_ADDPRODUCTCATEGORY";
                var conn = GetOpenConnection();

                var rowEffected = await SqlMapper.ExecuteAsync(conn, query, param: param, commandType: CommandType.StoredProcedure);

                return rowEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteProductCategoryAsync(int productId, int categoryId)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_ID", productId, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);
                param.Add(name: "CAT_ID", categoryId, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_DELETEPRODUCTCATEGORY";
                var conn = GetOpenConnection();

                var rowEffected = await SqlMapper.ExecuteAsync(conn, query, param: param, commandType: CommandType.StoredProcedure);

                return rowEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ImageViewModel> AddProductImage(int productId, string filePath)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "IMG_PRODUCT_ID", productId, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);
                param.Add(name: "IMG_FILE_PATH", filePath, dbType: OracleMappingType.Varchar2, direction: ParameterDirection.Input);
                param.Add(name: "IMG_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_ADDPRODUCTIMAGE";
                var conn = GetOpenConnection();

                var img = (await SqlMapper.QueryAsync<ImageViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return img;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ImageViewModel>> GetProductImage(int productId)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "IMG_PRODUCT_ID", productId, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);
                param.Add(name: "IMG_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETPRODUCTIMAGE";
                var conn = GetOpenConnection();

                var imgs = (await SqlMapper.QueryAsync<ImageViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();

                return imgs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteProductImage(int imgId)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "IMG_ID", imgId, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_DELETEPRODUCTIMAGE";
                var conn = GetOpenConnection();

                var rowEffected = await SqlMapper.ExecuteAsync(conn, query, param: param, commandType: CommandType.StoredProcedure);

                return rowEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ImageViewModel> GetImage(int imageId)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "IMG_ID", imageId, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);
                param.Add(name: "IMG_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETIMAGEBYID";
                var conn = GetOpenConnection();

                var img = (await SqlMapper.QueryAsync<ImageViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return img;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
