using eshop.core.Entities;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductViewModel>> GetAllProductAsync();
        Task<List<ProductCategoryViewModel>> GetAllProductWithCategoryAsync();
        Task<ProductViewModel> GetProductAsync(int id);
        Task<List<int>> GetProductCategoryAsync(int id);
        Task<ProductViewModel> AddProductAsync(Product p, List<CategoryViewModel> c);
        Task<ProductViewModel> UpdateProductAsync(int id, Product p, List<CategoryViewModel> categories);
        Task<int> DeleteProductAsync(int id);
        Task<int> AddProductCategoryAsync(int productId, int categoryId);
        Task<int> DeleteProductCategoryAsync(int productId, int categoryId);
        Task<ImageViewModel> AddProductImage(int productId, string filePath);
        Task<ImageViewModel> GetImage(int imageId);
        Task<List<ImageViewModel>> GetProductImage(int productId);
        Task<int> DeleteProductImage(int productId, int imgId);
    }
}
