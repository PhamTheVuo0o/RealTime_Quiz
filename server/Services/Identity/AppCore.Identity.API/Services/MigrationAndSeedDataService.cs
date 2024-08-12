using Microsoft.EntityFrameworkCore;
using AppCore.Identity.Infrastructure;
using AppCore.Identity.Infrastructure.SeedWork;
using AppCore.Infrastructure.Helpers;

namespace AppCore.Identity.API.Services
{
    public static class MigrationAndSeedDataService
    {
        public static async Task<WebApplication> ApplyMigrationsAndSeedData(this WebApplication app)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")} INF] Start Migrations and Seed Data.");
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                var unitOfWork = services.GetRequiredService<IUnitOfWork>();

                await context.Database.MigrateAsync();

                await SeedInitialAppUser.SeedData(context, unitOfWork);
            }
            catch (Exception ex)
            {
                LogHelper.Logger.LogError(ex, ex.Message);
                Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")} ERR] Migrations and Seed Data have err: {ex.Message}");
            }
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")} INF] End Migrations and Seed Data.");
            return app;
        }
    }
}
