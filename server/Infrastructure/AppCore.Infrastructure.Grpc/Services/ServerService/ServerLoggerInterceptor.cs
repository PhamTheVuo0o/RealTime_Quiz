using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AppCore.Infrastructure.Extensions;
using System.Net;

namespace AppCore.Infrastructure.Grpc.Services.ServerService
{
    public class ServerLoggerInterceptor : Interceptor
    {
        private readonly ILogger _logger;
        private readonly IHostEnvironment _env;

        public ServerLoggerInterceptor(ILogger<ServerLoggerInterceptor> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error thrown by {context.Method}.");
                // Convert the exception to a gRPC status exception
                throw new RpcException(new Status(StatusCode.Internal, _env.IsDevEnv()
                    ? $"{ex.Message}" : "An error occurred while processing the request."));
            }
        }
    }
}
