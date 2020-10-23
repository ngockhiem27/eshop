using eshop.infrastructure.RedisCache;
using System;
using System.Threading.Tasks;

namespace eshop.apiservices.Cache
{
    public abstract class BaseCached
    {
        protected readonly IRedisCache _cache;

        public BaseCached(IRedisCache cache)
        {
            _cache = cache;
        }

        protected string GetKey(string prefix, string name)
        {
            return prefix + name;
        }

        protected string GetKey(string prefix, string name, int id)
        {
            return prefix + name + id.ToString();
        }

        protected async Task<T> VerifyCache<T>(string cachedKey, Func<Task<T>> dbCallFunc)
        {
            var result = await _cache.GetAsync<T>(cachedKey);
            if (result == null)
            {
                result = await dbCallFunc();
                await _cache.SetAsync(cachedKey, result);
            }
            return result;
        }
    }
}
