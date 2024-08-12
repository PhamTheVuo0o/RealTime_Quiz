using AppCore.Infrastructure.Cache.Common;

namespace AppCore.Infrastructure.Cache.Contracts
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Set(string key, object data, int cacheTime = CacheConstant.DEFAULT_CACHE_TIME_IN_MINUTES);
        bool IsSet(string key);
        void Remove(string key);
        void Clear();
        void RemoveByPattern(string pattern);
    }
}