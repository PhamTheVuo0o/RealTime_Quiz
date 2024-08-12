using AppCore.Identity.Domain.Entities;
using AppCore.Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace AppCore.Identity.API.Services
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<TokenService>();

            return services;
        }
    }
}