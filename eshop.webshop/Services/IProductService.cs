using eshop.core.DTO.Request;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public interface IProductService
    {
        Task<(HttpStatusCode, List<ProductViewModel>)> GetAllProduct();
        Task<(HttpStatusCode, List<ProductViewModel>)> GetAllProductWithCategory();
        Task<(HttpStatusCode, List<ImageViewModel>)> GetProductImage(int id);
    }
}