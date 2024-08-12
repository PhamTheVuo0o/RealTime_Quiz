using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AppCore.Infrastructure.Grpc.Common;
using AppCore.Infrastructure.Grpc.Contracts.Core;
using AppCore.Infrastructure.Grpc.Contracts.Identity;
using AppCore.Infrastructure.Grpc.Enums;
using AppCore.Infrastructure.Grpc.Implementations.Core;
using AppCore.Infrastructure.Grpc.Implementations.Identity;
using AppCore.Infrastructure.Grpc.Services.ClientService.Base;

namespace AppCore.Infrastructure.Grpc.Services.ClientService
{
    public static class ConfigureGrpcClient
    {
        public static WebApplicationBuilder ConfigureGrpcClientServices(this WebApplicationBuilder builder, ServiceNameEnum currentService)
        {
            builder.Services.Configure<GrpcClientUrlSettings>(builder.Configuration.GetSection("GrpcClientUrlSettings"));
            builder.Services.AddTransient<BaseGrpcClient>();

            if(currentService != ServiceNameEnum.CoreService)
            {
                builder.Services.AddTransient<IMonitorCore, MonitorCore>();
            }

            if (currentService != ServiceNameEnum.IdentityService)
            {
                builder.Services.AddTransient<IMonitorIdentity, MonitorIdentity>();
            }
            return builder;
        }
    }
}
