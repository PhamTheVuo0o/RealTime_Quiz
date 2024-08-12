using AppCore.Infrastructure.MQTTClient.Common;
using AppCore.Infrastructure.MQTTClient.Contracts;
using AppCore.Infrastructure.MQTTClient.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MQTTnet.Client;
using MQTTnet;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;

namespace AppCore.Infrastructure.MQTTClient.Services
{
    public static class ConfigureMQTTClient
    {
        public static WebApplicationBuilder ConfigureMQTTClientServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MQTTClientSettings>(builder.Configuration.GetSection("MQTTClientSettings"));

            var config = new MQTTClientSettings();
            builder.Configuration.GetSection("MQTTClientSettings").Bind(config);

            builder.Services.AddSingleton<MqttClientOptions>(sp =>
            {

                var factory = new MqttFactory();
                var mqttClientOptionsBuilder = new MqttClientOptionsBuilder()
                    .WithTcpServer(config.Broker, config.Port)
                    .WithCredentials(config.Username, config.Password)
                    .WithClientId(config.ClientId ?? Guid.NewGuid().ToString())
                    .WithCleanSession();

                if (config.EnableSsl.HasValue && config.EnableSsl.Value)
                {
                    mqttClientOptionsBuilder = mqttClientOptionsBuilder.WithTls(o =>
                    {
                        o.CertificateValidationHandler = _ => true;
                        o.SslProtocol = SslProtocols.Tls12;

                        if (!string.IsNullOrEmpty(config.CertificatePath))
                        {
                            var certificate = new X509Certificate(config.CertificatePath);
                            o.Certificates = new List<X509Certificate> { certificate };
                        }
                    });
                }

                return mqttClientOptionsBuilder.Build();
            });

            // Register the MQTT client
            builder.Services.AddSingleton<IMqttClient>(sp =>
            {
                var factory = new MqttFactory();
                return factory.CreateMqttClient();
            });

            builder.Services.AddTransient<IMQTTClientFeature, MQTTClientFeature>();

            return builder;
        }
    }
}
