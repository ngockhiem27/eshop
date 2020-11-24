using System.Threading.Tasks;

namespace eshop.infrastructure.RedisCache
{
    public interface ICache
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync(string key, object data);
        Task RemoveAsync(string key);
    }
}
