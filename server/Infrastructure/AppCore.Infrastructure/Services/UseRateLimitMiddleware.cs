using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using AppCore.Infrastructure.Common;
using AppCore.Infrastructure.Middleware;


namespace AppCore.Infrastructure.Services
{
    public static class UseRateLimitMiddleware
    {
        public static WebApplication UseRestrictRequestRoute(this WebApplication app, IConfiguration config)
        {
            var rateLimitMiddlewareSetting = new RateLimitMiddlewareSetting();
            app.Configuration.GetSection("RateLimitMiddlewareSettings").Bind(rateLimitMiddlewareSetting);
            string[] routes = rateLimitMiddlewareSetting.RestricItems.Select(x => x.RestrictRequestEndPoints).ToArray();
            if (routes != null && routes.Count() > 0)
            {
                app.UseWhen(context => routes.Any(a => context.Request.Path.StartsWithSegments(a)), builder =>
                {
                    //Logging middleware
                    builder.UseMiddleware<RateLimitMiddleware>();
                });
            }
            return app;
        }
    }
}
