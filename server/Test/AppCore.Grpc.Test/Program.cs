using AppCore.Infrastructure.Grpc.Services.ClientService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AppCore.Infrastructure.Services;
using AppCore.Infrastructure.Grpc.Services.ClientService.Base;
using AppCore.Infrastructure.Grpc.Enums;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureGrpcClientServices(ServiceNameEnum.None);
builder.Services.AddTransient<BaseIdentityGrpcClient>();
builder.Services.AddTransient<BaseCoreGrpcClient>();

var app = builder.Build();

app.InitializeLogger<Program>(builder.Configuration);

// Delay for GRPC Server ready
bool IsDone = false;
while (!IsDone)
{
    try
    {
        // IdentityGrpcTest
        Console.WriteLine($"IdentityGrpcTest======================================");
        var identityGrpcClient = app.Services.GetRequiredService<BaseIdentityGrpcClient>();
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Testing : [{i}]");
            var reply1 = await identityGrpcClient.MonitorAsync();
            var reply2 = await identityGrpcClient.MonitorAsync();

            Console.WriteLine("IsIdentityService: " + reply1.IsServiceName);
            Console.WriteLine("DelayBetweenTwoRequest " + (reply2.CurrentTime - reply1.CurrentTime));
        }

        // CoreGrpcTest
        Console.WriteLine("");
        Console.WriteLine($"CoreGrpcTest======================================");
        var CoreGrpcClient = app.Services.GetRequiredService<BaseCoreGrpcClient>();
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Testing : [{i}]");
            var reply1 = await CoreGrpcClient.MonitorAsync();
            var reply2 = await CoreGrpcClient.MonitorAsync();

            Console.WriteLine("IsCoreService: " + reply1.IsServiceName);
            Console.WriteLine("DelayBetweenTwoRequest " + (reply2.CurrentTime - reply1.CurrentTime));
        }
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