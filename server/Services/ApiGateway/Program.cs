using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using AppCore.Infrastructure;
using Serilog;
using System;
using System.Runtime.InteropServices;
using System.Threading;

[assembly: CLSCompliant(false)]
[assembly: ComVisible(true)]
namespace ApiGateway.Generic
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(500, 500);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environmentName = hostingContext.HostingEnvironment.EnvironmentName;
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                        .AddJsonFile($"ocelot.{environmentName}.json", true, true)
                        .AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog(SeriLogger.Configure);
    }
}
