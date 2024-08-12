using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AppCore.Infrastructure.Helpers;
using Microsoft.Extensions.Hosting;
using Serilog;
using AppCore.Infrastructure.Extensions;
namespace AppCore.Infrastructure.Services
{
    public static class ConfigureLogger
    {
        public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();

            builder.Host.UseSerilog(SeriLogger.Configure);

            builder.AddCleanLogJob();

            return builder;
        }

        public static WebApplication InitializeLogger<T>(this WebApplication app, IConfiguration config)
        {
            var loggerFactory = app.Services.GetService<ILoggerFactory>();
            LogHelper.InitializeLogger<T>(loggerFactory);
            return app;
        }
    }
}
