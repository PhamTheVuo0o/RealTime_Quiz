using AppCore.Infrastructure.MQTTClient.Contracts;
using Microsoft.Extensions.Logging;

namespace AppCore.MQTTClient.Test.Implementations
{
    public class SubscribeEventHandle : ISubscribeEventHandle
    {
        private readonly ILogger<SubscribeEventHandle> _logger;
        public SubscribeEventHandle(ILogger<SubscribeEventHandle> logger)
        {
            _logger = logger;
        }

        public virtual async Task ProcessMessageAsync(string message, string topic)
        {
            try
            {
                Console.WriteLine($"Topic {topic} have message :{message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
            }
        }
    }
}
