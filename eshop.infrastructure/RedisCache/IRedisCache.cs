using System.Threading.Tasks;

namespace eshop.infrastructure.RedisCache
{
    public interface IRedisCache
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync(string key, object data, int expireAsMinute);
        Task RemoveAsync(string key);
    }
}
