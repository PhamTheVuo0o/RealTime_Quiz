namespace AppCore.Infrastructure.MQTTClient.Contracts
{
    public interface ISubscribeEventHandle
    {
        Task ProcessMessageAsync(string message, string topic);
    }
}
