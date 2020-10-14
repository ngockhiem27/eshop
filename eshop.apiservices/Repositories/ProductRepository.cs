using AutoMapper;
using Dapper;
using Dapper.Oracle;
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
        private readonly IMapper _mapper;

        public ProductRepository(IConfiguration configuration, IMapper mapper) : base(configuration)
        {
            _mapper = mapper;
        }

        public async Task<object> GetAllProductAsync()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETALLPRODUCT";
                var conn = GetOpenConnection();

                var result = await SqlMapper.QueryAsync<ProductViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetAllProductWithCategoryAsync()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add(name: "PRD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var query = PRODUCT_STORED_PACKAGE + ".SP_PRODUCT_GETALLPRODUCTWITHCATEGORY";
                var conn = GetOpenConnection();

                var result = await SqlMapper.QueryAsync<ProductCategoryViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure);
                var gr = result.GroupBy(r => r.Category_Id);

                var d = gr.ToList().First();

                var pv = _mapper.Map<IEnumerable<ProductCategoryViewModel>, IEnumerable<ProductViewModel>>(result);



                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
