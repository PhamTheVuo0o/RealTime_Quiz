using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppCore.Infrastructure.Cache.Common;
using AppCore.Infrastructure.Cache.Contracts;
using AppCore.Infrastructure.Cache.Implementations;
using AppCore.Infrastructure.Common.Constants;
using AppCore.Infrastructure.Extensions;

namespace AppCore.Infrastructure.Cache.Services
{
    public static class ConfigureCacheManager
    {
        public static WebApplicationBuilder ConfigureCacheManagerServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));
            var cacheSettings = new CacheSettings();
            builder.Configuration.GetSection("CacheSettings").Bind(cacheSettings);
            if (cacheSettings.UseDistributedCache)
            {
                // Distributed cache - such as Redis will be default
                builder.Services.AddSingleton<RedisCacheManager>();
                builder.Services.AddSingleton<ICacheManager, RedisCacheManager>();
            }
            else
            {
                // Memory cache will be default
                builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
                builder.Services.AddSingleton<ICacheManager, MemoryCacheManager>();
            }
            return builder;
        }
    }
}
