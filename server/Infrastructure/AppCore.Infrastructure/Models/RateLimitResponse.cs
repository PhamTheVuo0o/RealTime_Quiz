using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Infrastructure.Models
{
    public class RateLimitResponse : BaseResponse<RateLimitData>
    {
        public RateLimitResponse(int statusCode, int cycleTime, string message, string details = "") : 
            base(new RateLimitData(statusCode,cycleTime),false, message, details)
        {
        }
    }

    public class RateLimitData
    {
        public int StatusCode { get; set; }
        public int CycleTime { get; set; }
        public RateLimitData(int statusCode, int cycleTime)
        {
            StatusCode = statusCode;
            CycleTime = cycleTime;
        }
    }
}
