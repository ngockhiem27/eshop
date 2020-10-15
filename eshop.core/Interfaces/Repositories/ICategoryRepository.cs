using eshop.core.Entities;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryViewModel>> GetAllCategoryAsync();
        Task<List<ProductCategoryViewModel>> GetAllCategoryWithProductAsync();
        Task<CategoryViewModel> GetCategoryAsync(int id);
        Task<CategoryViewModel> AddCategoryAsync(Category category);
        Task<CategoryViewModel> UpdateCategoryAsync(int id, Category category);
        Task<int> DeleteCategoryAsync(int id);
    }
}
