using AppCore.Infrastructure.Grpc.Enums;
using AppCore.Infrastructure.Grpc.Services.ClientService;

namespace AppCore.Core.API.GrpcClient
{
    public static class InitializationGrpcClient
    {
        public static WebApplicationBuilder AddGrpcClient(this WebApplicationBuilder builder)
        {
            builder.ConfigureGrpcClientServices(ServiceNameEnum.None);

            return builder;
        }
    }
}
