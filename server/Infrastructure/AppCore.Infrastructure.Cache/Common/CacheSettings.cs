namespace AppCore.Infrastructure.Cache.Common
{
    public class CacheSettings
    {
        public int ShortTermCacheTimeInMinutes { get; set; }

        public int CacheTimeInMinutes { get; set; }

        public int LongTermCacheTimeInMinutes { get; set; }

        public bool UseDistributedCache { get; set; }

        public string RedisConnectionString { get; set; }
    }
}
