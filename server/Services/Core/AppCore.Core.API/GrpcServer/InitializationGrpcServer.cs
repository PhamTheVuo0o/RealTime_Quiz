using AppCore.Core.API.GrpcServer.MonitorService;
using AppCore.Infrastructure.Grpc.Services.ServerService;
using AppCore.Core.API.Application.Behavior;

namespace AppCore.Core.API.GrpcServer
{
    public static class InitializationGrpcServer
    {
        public static WebApplicationBuilder AddGrpcServer(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureGrpcServerServices(new Type[] { typeof(TransactionGrpcBehaviour) });

            builder.Services.AddTransient<TransactionGrpcBehaviour>();

            return builder;
        }
        public static WebApplication MapGrpcServices(this WebApplication app)
        {
            app.MapGrpcService<PingService>();
            return app;
        }
    }
}
