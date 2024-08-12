using AppCore.Infrastructure.MQTTClient.Contracts;
using AppCore.Infrastructure.MQTTClient.Services;
using AppCore.MQTTClient.Test;
using AppCore.MQTTClient.Test.Implementations;
using MessagePack;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ISubscribeEventHandle, SubscribeEventHandle>();
builder.ConfigureMQTTClientServices();

var app = builder.Build();
var mQTTClientFeature = app.Services.GetRequiredService<IMQTTClientFeature>();
await mQTTClientFeature.Connect();
string topic = "testtopic";
await mQTTClientFeature.Subscribe(topic);

for(int i = 0; i < 10; i++)
{
    var person = new Person { Id = i, Name = "John Doe" };
    person.Test(person);
}

bool IsDone = false;
while (!IsDone)
{
    try
    {
        // MQTTClient
        
        if (mQTTClientFeature != null)
        {
            await mQTTClientFeature.Publish(topic, $"{DateTime.Now.ToString("ssfff")} HELLO");
        }

    }
    catch (Exception ex)
    {
        Thread.Sleep(1000);
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine("");
        Console.WriteLine("Press any key to try again...");
        Console.ReadKey();
        IsDone = true;
    }
}