using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using AppCore.Infrastructure.Extensions;
using AppCore.Infrastructure.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AppCore.Infrastructure.Common;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AppCore.Infrastructure.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitMiddleware> _logger;
        private readonly ConcurrentDictionary<string, DateTime> _requestDateTimes = new ConcurrentDictionary<string, DateTime>();
        private readonly ConcurrentDictionary<string, int> _requestDateCount = new ConcurrentDictionary<string, int>();

        public RateLimitMiddleware(RequestDelegate next,
            ILogger<RateLimitMiddleware> logger) {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            // Use IP address and Request Path as the client identifier
            string clientId = $"{context.Connection.RemoteIpAddress?.ToString()}_{context.Request.Path}";
            var hasRequestCount = _requestDateCount.TryGetValue(clientId, out int requestCount);
            requestCount ++;
            if ((hasRequestCount)&&(requestCount>1))
            {
                var hasLastRequestTime = _requestDateTimes.TryGetValue(clientId, out DateTime lastRequestTime);
                if (hasLastRequestTime)
                {
                    int cycleTime = GetCycleTimeByEndpoint(context);
                    if (DateTime.Now < lastRequestTime.AddSeconds(cycleTime))
                    {
                        string message = $"Rate limit exceeded. Please try again later after {cycleTime} seconds.";
                        _logger.LogInformation(message);

                        context.Response.StatusCode = 429;
                        context.Response.ContentType = "application/json";

                        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                        var response = new RateLimitResponse(context.Response.StatusCode,cycleTime, message);
                        var json = JsonSerializer.Serialize(response, options);

                        await context.Response.WriteAsync(json);
                        return;
                    }
                    else
                    {
                        requestCount = 0;
                    }
                }
                _requestDateTimes.AddOrUpdate(clientId, DateTime.Now, (_, now) => DateTime.Now);
            }
            _requestDateCount.AddOrUpdate(clientId, requestCount, (_, now) => requestCount);
            await _next(context);
        }

        private int GetCycleTimeByEndpoint(HttpContext context)
        {
            var endpoint = context.Request.Path;
            var config = context.RequestServices.GetRequiredService<RateLimitMiddlewareSetting>();
            if (config != null)
            {
                var restricItem = config.RestricItems.Where(x => x.RestrictRequestEndPoints.Contains(endpoint)).FirstOrDefault();

                // Check if endpoint is available
                if (restricItem != null)
                {
                    return restricItem.RestrictCycleTimeSeconds;
                }
            }
            return 60;
        }
    }
}
