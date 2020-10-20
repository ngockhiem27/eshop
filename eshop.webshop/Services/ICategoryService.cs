using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public interface ICategoryService
    {
        Task<(HttpStatusCode, List<CategoryViewModel>)> GetAllCategory();
        Task<(HttpStatusCode, List<CategoryViewModel>)> GetAllCategoryWithProduct();
    }
}