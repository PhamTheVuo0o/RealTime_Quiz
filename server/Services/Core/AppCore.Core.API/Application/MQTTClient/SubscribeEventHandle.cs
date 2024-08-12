using AppCore.Infrastructure.MQTTClient.Contracts;

namespace AppCore.Core.API.Application.MQTTClient
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
                _logger.LogInformation($"Topic {topic} have message :{message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
            }
        }
    }
}
