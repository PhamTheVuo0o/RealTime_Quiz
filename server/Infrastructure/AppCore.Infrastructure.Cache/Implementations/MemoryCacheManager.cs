using Microsoft.Extensions.Caching.Memory;
using AppCore.Infrastructure.Cache.Contracts;
using System;

namespace AppCore.Infrastructure.Cache.Implementations
{
    public class MemoryCacheManager : BaseCacheManager, ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            return _memoryCache.TryGetValue(key, out T data) ? data : default;
        }

        public void Set(string key, object data, int cacheTimeInMinutes)
        {
            if (data == null)
                return;
            _memoryCache.Set(key, data, TimeSpan.FromMinutes(cacheTimeInMinutes));
        }

        public virtual void Set(string key, object data)
        {
            Set(key, data, DEFAULT_CACHE_TIME_IN_MINUTES);
        }

        public bool IsSet(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            if (IsSet(key))
            {
                _memoryCache.Remove(key);
            }
        }

        public void Clear()
        {
            // Notthing
        }

        public void RemoveByPattern(string pattern)
        {
            // Notthing
        }
    }
}
