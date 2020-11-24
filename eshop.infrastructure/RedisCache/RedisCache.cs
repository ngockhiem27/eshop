using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.infrastructure.RedisCache
{
    public class RedisCache : ICache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly RedisConfig _config;

        public RedisCache(IDistributedCache distributedCache, RedisConfig config)
        {
            _distributedCache = distributedCache;
            _config = config;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            try
            {
                var encodedData = await _distributedCache.GetAsync(key);
                if (encodedData == null) return default;
                return Deserialize<T>(encodedData);
            } catch
            {
                return default;
            }
        }

        public async Task SetAsync(string key, object data)
        {
            try
            {
                var encodedData = Serialize(data);
                await _distributedCache.SetAsync(key, encodedData, new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(_config.ExpireMinutes)
                });
            }
            catch { }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                await _distributedCache.RemoveAsync(key);
            }
            catch { }
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
