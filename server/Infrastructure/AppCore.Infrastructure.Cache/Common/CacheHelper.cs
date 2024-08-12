namespace AppCore.Infrastructure.Cache.Common
{
    public static class CacheHelper
    {
        public static string BuildCacheKey(string prefix, string detail)
        {
            return $"{prefix}_{detail}";
        }

        public static int FindCacheTimeByStrategy(this CacheSettings cacheSettings, short? cacheStrategy = null)
        {
            if (cacheStrategy == CacheStrategyConstant.SHORT_TERM)
            {
                return cacheSettings.ShortTermCacheTimeInMinutes;
            }
            else if (cacheStrategy == CacheStrategyConstant.LONG_TERM)
            {
                return cacheSettings.LongTermCacheTimeInMinutes;
            }
            return cacheSettings.CacheTimeInMinutes;
        }
    }
}
