using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using AppCore.Infrastructure.Common;
using HangfireBasicAuthenticationFilter;

namespace AppCore.Infrastructure.Persistence.Services
{
    public static class ConfigureHangfire
    {
        public static WebApplicationBuilder AddHangfire(this WebApplicationBuilder builder, IConfiguration config)
        {
            var appSetting = new AppSetting();
            config.GetSection("AppSetting").Bind(appSetting);

            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(option =>
                {
                    option.UseNpgsqlConnection(appSetting.GetDefaultConnection);
                }));

            builder.Services.AddHangfireServer();
            return builder;
        }
        public static IApplicationBuilder UseHangfire(this IApplicationBuilder app, string ServiceName)
        {
            var appSetting = app.ApplicationServices.GetRequiredService<IOptions<AppSetting>>();

            app.UseHangfireDashboard($"/{ServiceName}-hf", new DashboardOptions
            {
                Authorization = new[] {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = appSetting.Value.GetHangfireUserName,
                        Pass = appSetting.Value.GetHangfirePassword
                    }
                }
            });

            return app;
        }
    }
}
