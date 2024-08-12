using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MonitorService.Protos;
using AppCore.Infrastructure.Grpc.Enums;
using AppCore.Infrastructure.Grpc.Protos.MonitorService.Ping.Services;

namespace AppCore.Core.API.GrpcServer.MonitorService
{
    public class PingService : BasePingService
    {
        private readonly ILogger<PingService> _logger;
        public PingService(ILogger<PingService> logger)
        {
            _logger = logger;
        }
        public override Task<PingResponse> Ping(PingRequest request, ServerCallContext context)
        {
            return Task.FromResult(new PingResponse
            {
                IsServiceName = request.ServiceName == ServiceNameEnum.CoreService.ToString(),
                CurrentTime = Timestamp.FromDateTime(DateTime.UtcNow)
            });
        }
    }
}
