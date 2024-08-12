using AppCore.Infrastructure.Cache.Contracts;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using AppCore.Infrastructure.Cache.Extensions;
using System.Data.Common;
using Microsoft.Extensions.Options;
using AppCore.Infrastructure.Cache.Common;

namespace AppCore.Infrastructure.Cache.Implementations
{
    public class RedisCacheManager : BaseCacheManager, ICacheManager
    {
        private readonly IOptions<CacheSettings> _config;
        private ConnectionMultiplexer _Connection;
        private readonly string _redisConnectionString;
        public RedisCacheManager(IOptions<CacheSettings> config)
        {
            _config = config;
            _redisConnectionString = config.Value.RedisConnectionString;
            _Connection = ConnectionMultiplexer.Connect(_redisConnectionString);
        }

        public ConnectionMultiplexer Connection()
        {
            if ((_Connection != null) && (_Connection.IsConnected))
            {
                return _Connection;
            }
            else
            {
                return ConnectionMultiplexer.Connect(_redisConnectionString);
            }
        }

        public T Get<T>(string key)
        {
            return Connection().GetDatabase().Get<T>(key);
        }

        public void Set(string key, object data, int cacheTimeInMinutes)
        {
            if (data == null)
                return;
            Connection().GetDatabase().Set(key, data);
            Connection().GetDatabase().KeyExpire(key, DateTime.Now + TimeSpan.FromMinutes(cacheTimeInMinutes));
        }

        public virtual void Set(string key, object data)
        {
            Set(key, data, DEFAULT_CACHE_TIME_IN_MINUTES);
        }

        public bool IsSet(string key)
        {
            return Connection().GetDatabase().KeyExists(key);
        }

        public void Remove(string key)
        {
            Connection().GetDatabase().KeyDelete(key);
        }

        public void Clear()
        {
            var endpoint = Connection().GetEndPoints();
            Connection().GetServer(endpoint.FirstOrDefault()).FlushDatabase();
        }

        public void RemoveByPattern(string pattern)
        {
            foreach (var ep in Connection().GetEndPoints())
            {
                var server = Connection().GetServer(ep);
                var keys = server.Keys(pattern: "*" + pattern + "*");
                foreach (var key in keys)
                    Connection().GetDatabase().KeyDelete(key);
            }
        }
    }
}