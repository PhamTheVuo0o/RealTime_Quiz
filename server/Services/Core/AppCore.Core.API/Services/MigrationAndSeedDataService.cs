using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppCore.Core.Domain.Entities;
using AppCore.Core.Infrastructure;
using AppCore.Core.Infrastructure.SeedWork;
using AppCore.Infrastructure.Helpers;

namespace AppCore.Core.API.Services
{
    public static class MigrationAndSeedDataService
    {
        public static async Task<WebApplication> ApplyMigrationsAndSeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                var unitOfWork = services.GetRequiredService<IUnitOfWork>();

                await context.Database.MigrateAsync();

                await SeedInitialQuizs.SeedData(context, unitOfWork);

                await SeedInitialQuestions.SeedData(context, unitOfWork);
            }
            catch (Exception ex)
            {
                LogHelper.Logger.LogError(ex, ex.Message);
            }
            return app;
        }
    }
}
