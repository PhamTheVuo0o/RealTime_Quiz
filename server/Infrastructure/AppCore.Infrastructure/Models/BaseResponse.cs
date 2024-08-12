using AppCore.Infrastructure.Enums;

namespace AppCore.Infrastructure.Models
{
    public class BaseResponse<T>
    {
        public BaseResponse(T data = default, bool isSuccess = false, string errorMessage = "", string details = "", ErrorTypeEnum errorType = ErrorTypeEnum.None)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Details = details;
            ErrorType = errorType;
            Data = data;
        }        
        public bool IsSuccess {  get; set; }
        public string ErrorMessage { get; set; }
        public ErrorTypeEnum ErrorType { get; set; }
        public string Details { get; set; }
        public T Data { get; set; }
    }
}