using Microsoft.Extensions.DependencyInjection;
using AppCore.Identity.Infrastructure.IRepositories.Account;
using AppCore.Identity.Infrastructure.Repositories.Account;

namespace AppCore.Identity.Infrastructure
{
    public static class DataBootstrapper
    {
        public static void AddDataBootstrapper(this IServiceCollection services)
        {
            services.AddTransient<IAppUserRepository, AppUserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
