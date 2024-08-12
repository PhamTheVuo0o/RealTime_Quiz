using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Infrastructure.Models
{
    public class ExceptionResponse : BaseResponse<ExceptionData>
    {
        public ExceptionResponse(int statusCode, string message, string details = "") : base(new ExceptionData(statusCode), false, message, details)
        {
        }
    }
    public class ExceptionData
    {
        public int StatusCode { get; set; }
        public ExceptionData(int statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
