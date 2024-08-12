using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AppCore.Core.API.Application.Behavior;
using AppCore.Core.Infrastructure;
using AppCore.Infrastructure.Middleware;
using AppCore.Infrastructure.Services;
using MediatR;
using AutoMapper;
using AppCore.Infrastructure.Extensions;
using AppCore.Core.API.GrpcServer;
using AppCore.Core.API.GrpcClient;
using AppCore.Infrastructure.Persistence.AppDbContext;
using AppCore.Core.API.Services;
using AppCore.Infrastructure.Common;
using AppCore.Infrastructure.Persistence.Services;
using AppCore.Infrastructure.Cache.Services;
using AppCore.Infrastructure.MQTTClient.Services;
using AppCore.Infrastructure.MQTTClient.Contracts;
using AppCore.Core.API.Application.MQTTClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IBaseDbContext, DataContext>(options =>
            options.AddCustomNpgsqlDbOptions(builder.Configuration));

builder.Services.AddCorsPolicyServices(builder.Configuration);

builder.Services.AddSingleton(new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
}).CreateMapper());

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDataBootstrapper();

builder.Services.RegisterPipelineBehaviors();

builder.AddGrpcServer();

builder.AddGrpcClient();

builder.ConfigureCacheManagerServices();

builder.Services.AddTransient<ISubscribeEventHandle, SubscribeEventHandle>();

builder.ConfigureMQTTClientServices();

builder.AddLogger();

var app = builder.Build();

Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")} INF] App Ver 20240507 Run With " +
    $"{app.Environment.EnvironmentName} environment.");

app.UseCorsPolicyServices();

app.InitializeLogger<Program>(builder.Configuration);

app.UseMiddleware<SecureHeadersMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevEnv())
{
    app.UseSwagger();

    app.UseSwaggerUI();

    await app.ApplyMigrationsAndSeedData();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcServices();

app.Run();