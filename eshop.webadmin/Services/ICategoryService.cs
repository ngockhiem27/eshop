using eshop.core.DTO.Request;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public interface ICategoryService
    {
        Task<(HttpStatusCode, List<CategoryViewModel>)> GetAllCategory();

        Task<(HttpStatusCode, List<CategoryViewModel>)> GetAllCategoryWithProduct();

        Task<(HttpStatusCode, CategoryViewModel)> AddCategory(CategoryInfoRequest categoryRequest);

        Task<(HttpStatusCode, CategoryViewModel)> UpdateCategory(CategoryInfoRequest categoryRequest);

        Task<HttpStatusCode> DeleteCategory(int id);
    }
}