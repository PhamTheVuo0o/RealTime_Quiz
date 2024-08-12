namespace AppCore.Infrastructure.Grpc.Model
{
    public class PingGrpcResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public PingGrpcResponse()
        {
            IsSuccess = false;
            Message = string.Empty;
        }
    }
}
