using AppCore.Infrastructure.Models;

namespace AppCore.Infrastructure.Common
{
    public class RateLimitMiddlewareSetting
    {
        public List<RestricItem>? RestricItems { get; set; }
        public RateLimitMiddlewareSetting()
        {
            RestricItems = new List<RestricItem>();
        }
    }
}
