using AppCore.Identity.API.Jobs.Hangfire.Contracts;
using AppCore.Infrastructure.Persistence.Services;

namespace AppCore.Identity.API.Jobs.Hangfire
{
    public static class InitializationHangfireServer
    {
        public static WebApplicationBuilder AddHangfireServer(this WebApplicationBuilder builder)
        {
            builder.AddHangfire(builder.Configuration);

            builder.Services.AddTransient<IBackgroundJobService, BackgroundJobService>();
            builder.Services.AddTransient<HangfireJob>();

            return builder;
        }
    }
}
