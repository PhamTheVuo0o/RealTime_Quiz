using AppCore.Infrastructure.MQTTClient.Common;
using AppCore.Infrastructure.MQTTClient.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System.Text;
using System.Text.Json;

namespace AppCore.Infrastructure.MQTTClient.Implementations
{
    public class MQTTClientFeature : IMQTTClientFeature
    {
        private readonly IOptions<MQTTClientSettings> _config;
        private readonly ISubscribeEventHandle _subscribeEventHandle;
        private readonly ILogger<MQTTClientFeature> _logger;
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _mqttClientOptions;
        public MQTTClientFeature(IOptions<MQTTClientSettings> config,
            ISubscribeEventHandle subscribeEventHandle,
            ILogger<MQTTClientFeature> logger,
            IMqttClient mqttClient, 
            MqttClientOptions mqttClientOptions) {
            _config = config;
            _subscribeEventHandle = subscribeEventHandle;
            _logger = logger;
            _mqttClient = mqttClient;
            _mqttClientOptions = mqttClientOptions;
        }

        public async Task<bool> Connect()
        {
            try
            {
                if (!_mqttClient.IsConnected)
                {
                    await _mqttClient.ConnectAsync(_mqttClientOptions);
                }

                return _mqttClient.IsConnected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> Subscribe(string topic)
        {
            try
            {
                await _mqttClient.SubscribeAsync(topic);

                Func<string, string, Task> messageReceivedHandler = (message, topic) =>
                {
                    return _subscribeEventHandle.ProcessMessageAsync(message, topic);
                };

                _mqttClient.ApplicationMessageReceivedAsync += async e =>
                {
                    var handleMessageTask = messageReceivedHandler(Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment), topic);
                    await handleMessageTask.ConfigureAwait(false);
                };
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> Unsubscribe(string topic)
        {
            try
            {
                await _mqttClient.UnsubscribeAsync(topic);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> Disconnect()
        {
            try
            {
                if ((_mqttClient != null)&&(_mqttClient.IsConnected))
                {
                    await _mqttClient.DisconnectAsync();
                }

                return (_mqttClient != null) && (!_mqttClient.IsConnected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> Publish<T>(string topic, T message)
        {
            try
            {
                string msg = JsonSerializer.Serialize(message);

                return await Publish(topic,msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing message to topic '{topic}': {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Publish(string topic, string message)
        {
            try
            {
                var mqttMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(message)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                    .WithRetainFlag()
                    .Build();

                await _mqttClient.PublishAsync(mqttMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing message to topic '{topic}': {ex.Message}");
                return false;
            }
        }
    }
}
