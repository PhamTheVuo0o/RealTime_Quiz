using AppCore.Infrastructure.Grpc.Model;

namespace AppCore.Infrastructure.Grpc.Contracts.Identity
{
    public interface IMonitorIdentity
    {
        Task<PingGrpcResponse> PingAsync();
    }
}
