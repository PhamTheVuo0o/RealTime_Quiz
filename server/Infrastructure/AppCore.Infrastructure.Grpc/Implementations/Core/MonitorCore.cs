using AppCore.Infrastructure.Grpc.Contracts.Core;
using AppCore.Infrastructure.Grpc.Model;
using AppCore.Infrastructure.Grpc.Services.ClientService.Base;

namespace AppCore.Infrastructure.Grpc.Implementations.Core
{
    public class MonitorCore : BaseCoreGrpcClient, IMonitorCore
    {
        public MonitorCore(BaseGrpcClient baseGrpcClient) : base(baseGrpcClient) { 
        }
        public async Task<PingGrpcResponse> PingAsync()
        {
            PingGrpcResponse rlt = new PingGrpcResponse();
            try
            {
                var reply1 = await MonitorAsync();
                var reply2 = await MonitorAsync();
                rlt.Message = reply1.IsServiceName ? $"{serviceNameEnum.ToString()}; Delay Between Two Requests: {reply2.CurrentTime - reply1.CurrentTime}" : "";
                rlt.IsSuccess = true;
            }
            catch (Exception ex)
            {
                rlt.Message = ex.Message;
            }
            return rlt;
        }
    }
}
