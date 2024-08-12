using AppCore.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AppCore.Infrastructure.Cache.Services;
using Microsoft.Extensions.Options;
using AppCore.Infrastructure.Cache.Common;
using AppCore.Infrastructure.Common.Constants;
using AppCore.Cache.Test.Implementations;
using AppCore.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureCacheManagerServices();
builder.Services.AddTransient<RedisCacheTest>();
builder.Services.AddTransient<CacheTest>();

var app = builder.Build();

app.InitializeLogger<Program>(builder.Configuration);

bool IsDone = false;
while (!IsDone)
{
    try
    {
        IOptions<CacheSettings> cacheSettings = app.Services.GetRequiredService<IOptions<CacheSettings>>();
        var memoryCacheTest = app.Services.GetRequiredService<CacheTest>();
        memoryCacheTest.Test();

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        IsDone = true;
    }
    catch (Exception ex)
    {
        Thread.Sleep(1000);
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine("");
        Console.WriteLine("Press any key to try again...");
        Console.ReadKey();
    }
}