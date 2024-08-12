using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AppCore.Identity.API.Application.Behavior;
using AppCore.Identity.API.Services;
using AppCore.Identity.Infrastructure;
using AppCore.Infrastructure.Middleware;
using AppCore.Infrastructure.Services;
using MediatR;
using AutoMapper;
using AppCore.Identity.API.GrpcServer;
using AppCore.Infrastructure.Persistence.AppDbContext;
using AppCore.Infrastructure.Extensions;
using AppCore.Infrastructure.Common;
using AppCore.Infrastructure.Persistence.Services;
using AppCore.Identity.API.GrpcClient;
using AppCore.Identity.API.Jobs.Hangfire;

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

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddCorsPolicyServices(builder.Configuration);

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddCustomAuthorization();

builder.Services.AddSingleton(new MapperConfiguration(mc => 
{
    mc.AddProfile(new MappingProfile());
}).CreateMapper());

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDataBootstrapper();

builder.Services.AddOptions();

builder.Services.RegisterPipelineBehaviors();

builder.AddHangfireServer();

builder.AddGrpcServer();

builder.AddGrpcClient();

builder.AddLogger();

builder.AddRateLimitMiddlewareSetting();

var app = builder.Build();

Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")} INF] App Run With " +
    $"{app.Environment.EnvironmentName} environment.");

app.UseHangfire("Identity");

app.UseCorsPolicyServices();

app.InitializeLogger<Program>(builder.Configuration);

app.UseMiddleware<SecureHeadersMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

app.UseRestrictRequestRoute(builder.Configuration);

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
