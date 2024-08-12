using MonitorService.Protos;
using AppCore.Infrastructure.Grpc.Enums;

namespace AppCore.Infrastructure.Grpc.Services.ClientService.Base
{
    public class BaseCoreGrpcClient
    {
        public readonly BaseGrpcClient _baseGrpcClient;
        public const ServiceNameEnum serviceNameEnum = ServiceNameEnum.CoreService;
        public BaseCoreGrpcClient(BaseGrpcClient baseGrpcClient)
        {
            _baseGrpcClient = baseGrpcClient;
        }
        public GrpcPingServices.GrpcPingServicesClient GetPingServicesClient()
        {
            var invoker = _baseGrpcClient.GetInvoker(serviceNameEnum);
            return new GrpcPingServices.GrpcPingServicesClient(invoker);
        }
        public async Task<PingResponse> MonitorAsync()
        {
            return await GetPingServicesClient().PingAsync(new PingRequest
            { ServiceName = serviceNameEnum.ToString() });
        }
    }
}
