using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AppCore.Infrastructure.Grpc.Common;
using AppCore.Infrastructure.Grpc.Enums;

namespace AppCore.Infrastructure.Grpc.Services.ClientService.Base
{
    public class BaseGrpcClient
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IOptions<GrpcClientUrlSettings> _config;
        private readonly IHostEnvironment _env;

        public BaseGrpcClient(ILoggerFactory loggerFactory, IOptions<GrpcClientUrlSettings> config, IHostEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _config = config;
            _env = env;
        }

        public CallInvoker GetInvoker(ServiceNameEnum serviceName)
        {
            string? url = _config.Value.GetUrlByServiceNameEnum(serviceName);
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException($"url of Grpc server of {serviceName.ToString()} is null or empty");
            }
            else
            {
                var channel = GrpcChannel.ForAddress(url);
                return channel.Intercept(new ClientLoggerInterceptor(_loggerFactory, _env));
            }
        }
    }
}
