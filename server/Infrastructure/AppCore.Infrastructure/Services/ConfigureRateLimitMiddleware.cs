using Microsoft.AspNetCore.Builder;
using AppCore.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppCore.Infrastructure.Middleware;
using Microsoft.Extensions.Options;

namespace AppCore.Infrastructure.Services
{
    public static class ConfigureRateLimitMiddleware
    {
        public static WebApplicationBuilder AddRateLimitMiddlewareSetting(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<RateLimitMiddlewareSetting>(builder.Configuration.GetSection("RateLimitMiddlewareSettings"));
            builder.Services.AddSingleton<RateLimitMiddlewareSetting>(provider =>
            {
                // Load configuration from appsettings.json or other configuration source
                return provider.GetRequiredService<IOptions<RateLimitMiddlewareSetting>>().Value;
            });
            return builder;
        }
    }
}
