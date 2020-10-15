using eshop.core.DTO.Request;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public interface IProductService
    {
        Task<(HttpStatusCode, List<ProductViewModel>)> GetAllProduct();

        Task<(HttpStatusCode, List<ProductViewModel>)> GetAllProductWithCategory();

        Task<(HttpStatusCode, ProductViewModel)> AddProduct(ProductInfoRequest productRequest);
        Task<(HttpStatusCode, ProductViewModel)> UpdateProduct(ProductInfoRequest productRequest);

        Task<HttpStatusCode> DeleteProduct(int id);
    }
}