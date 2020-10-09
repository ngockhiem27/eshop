using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eshop.infrastructure.RedisCache
{
    public static class RedisCacheExtension
    {
        public static void AddRedisCache(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDistributedRedisCache(opt =>
            {
                opt.Configuration = Configuration.GetConnectionString("Redis");
            });
            services.AddScoped<IRedisCache, RedisCache>();
        }
    }
}
