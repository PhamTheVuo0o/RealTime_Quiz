using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AppCore.Infrastructure.Common;
using AppCore.Infrastructure.Jobs;
using Quartz;
using AppCore.Infrastructure.Extensions;

namespace AppCore.Infrastructure.Services
{
    public static class ConfigureCleanLogJob
    {
        public static WebApplicationBuilder AddCleanLogJob(this WebApplicationBuilder builder)
        {
            builder.Services.AddCleanLogJobToService(builder.Configuration);
            return builder;
        }

        public static IServiceCollection AddCleanLogJobToService(this IServiceCollection services, IConfiguration config)
        {
            var logPath = config.GetValue<string>("Logging:LogFilePath");
            if (!string.IsNullOrEmpty(logPath))
            {
                services.Configure<CleanLogSetting>(config.GetSection("Logging"));

                var cleanLogSettings = new CleanLogSetting();
                config.GetSection("CleanLogSettings").Bind(cleanLogSettings);

                services.AddQuartz(q =>
                {
                    var cleanLogJobKey = new JobKey("CleanLogJob");
                    q.AddJob<CleanLogJob>(opts => opts.WithIdentity(cleanLogJobKey));

                    q.AddTrigger(opts => opts
                        .ForJob(cleanLogJobKey)
                        .WithIdentity("CleanLogJob-trigger")
                        .WithCronSchedule(cleanLogSettings.GetCronSchedule())
                    );
                });
                services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            }
            return services;
        }
    }
}
