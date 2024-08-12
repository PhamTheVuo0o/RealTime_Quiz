using AppCore.Infrastructure.Grpc.Model;

namespace AppCore.Infrastructure.Grpc.Contracts.Core
{
    public interface IMonitorCore
    {
        Task<PingGrpcResponse> PingAsync();
    }
}
