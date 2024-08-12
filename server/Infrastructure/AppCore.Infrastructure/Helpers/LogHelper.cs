using Microsoft.Extensions.Logging;


namespace AppCore.Infrastructure.Helpers
{
    public static class LogHelper
    {
        private static ILogger _logger;

        public static void InitializeLogger<T>(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public static ILogger Logger => _logger;
    }
}
