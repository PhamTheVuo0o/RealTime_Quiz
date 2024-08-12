namespace AppCore.Infrastructure.MQTTClient.Common
{
    public class MQTTClientSettings
    {
        public string? Broker { get; set; }
        public int? Port { get; set; }
        public string? ClientId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? EnableSsl { get; set; }
        public string? CertificatePath { get; set; }
    }
}
