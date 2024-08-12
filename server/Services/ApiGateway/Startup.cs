using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using AppCore.Infrastructure;
using AppCore.Infrastructure.Common;
using AppCore.Infrastructure.Extensions;
using AppCore.Infrastructure.Services;

namespace ApiGateway.Generic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));

            services.AddCorsPolicyServices(Configuration);

            services.AddOcelot(Configuration)
                    .AddPolly();

            services.AddSwaggerForOcelot(Configuration);

            services.AddControllers();

            services.AddHealthChecks();

            services.AddCleanLogJobToService(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "All API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static async void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevEnv())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorsPolicyServices();

            app.UseRouting();
            app.UseSwagger();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
            app.UseStaticFiles();

            await app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            })
            .UseOcelot().ConfigureAwait(false);
        }
    }
}