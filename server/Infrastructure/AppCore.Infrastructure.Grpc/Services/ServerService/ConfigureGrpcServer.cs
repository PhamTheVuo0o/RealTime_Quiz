using Microsoft.Extensions.DependencyInjection;

namespace AppCore.Infrastructure.Grpc.Services.ServerService
{
    public static class ConfigureGrpcServer
    {
        public static IServiceCollection ConfigureGrpcServerServices(this IServiceCollection services, Type[] additionalInterceptors = null)
        {
            services.AddGrpc(options =>
            {
                options.Interceptors.Add<ServerLoggerInterceptor>();
                if (additionalInterceptors != null)
                {
                    foreach (var interceptorType in additionalInterceptors)
                    {
                        options.Interceptors.Add(interceptorType);
                    }
                }
            });
            services.AddSingleton<ServerLoggerInterceptor>();
            return services;
        }
    }
}
