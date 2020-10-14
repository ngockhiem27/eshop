using System.Threading.Tasks;

namespace eshop.core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<object> GetAllProductAsync();
        Task<object> GetAllProductWithCategoryAsync();
    }
}
