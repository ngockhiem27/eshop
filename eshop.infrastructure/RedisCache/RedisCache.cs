using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.infrastructure.RedisCache
{
    public class RedisCache : IRedisCache
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var encodedData = await _distributedCache.GetAsync(key);
            if (encodedData == null) return default;
            return Deserialize<T>(encodedData);
        }

        public async Task SetAsync(string key, object data, int expireAsMinute)
        {
            var encodedData = Serialize(data);
            await _distributedCache.SetAsync(key, encodedData, new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(expireAsMinute)
            });
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        private byte[] Serialize(object data)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
        }

        private T Deserialize<T>(byte[] encodedData)
        {
            var serializedData = Encoding.UTF8.GetString(encodedData);
            return JsonSerializer.Deserialize<T>(serializedData);
        }
    }
}
