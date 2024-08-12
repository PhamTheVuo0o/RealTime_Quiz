namespace AppCore.Infrastructure.MQTTClient.Contracts
{
    public interface IMQTTClientFeature
    {
        Task<bool> Connect();
        Task<bool> Subscribe(string topic);
        Task<bool> Unsubscribe(string topic);
        Task<bool> Disconnect();
        Task<bool> Publish(string topic, string message);
        Task<bool> Publish<T>(string topic, T message);
    }
}
