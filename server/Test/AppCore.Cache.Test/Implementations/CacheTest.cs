using AppCore.Infrastructure.Cache.Contracts;

namespace AppCore.Cache.Test.Implementations
{
    public class CacheTest
    {
        private readonly ICacheManager _cacheManager;
        public CacheTest(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        public void Test()
        {
            DateTime oldTime = DateTime.Now;
            DateTime firtTime = DateTime.Now;
            int n = 0;
            bool isDone = false;
            // Set data in the cache with a specific key and expiration time
            while (!isDone)
            {
                string keyname = $"key_{firtTime.ToString("yyyyMMddHHmmssffff")}_{DateTime.Now.ToString("yyyyMMddHHmmssffff")}";
                _cacheManager.Set(keyname, keyname);

                // Get data from the cache using the key
                string cachedData = _cacheManager.Get<string>(keyname);
                _cacheManager.Remove(keyname);
                string cacheDataAfterRemove = _cacheManager.Get<string>(keyname);
                if (cachedData.Contains(keyname) & string.IsNullOrEmpty(cacheDataAfterRemove))
                    n++;

                if ((DateTime.Now - oldTime).TotalSeconds > 1)
                {
                    Console.WriteLine($"Total cache times :{n}/s");
                    oldTime = DateTime.Now;
                    n = 0;
                }
            }
        }
    }
}
