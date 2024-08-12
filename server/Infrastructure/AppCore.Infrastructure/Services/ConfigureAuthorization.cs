using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using AppCore.Infrastructure.Security;

namespace AppCore.Infrastructure.Services
{
    public static class ConfigureAuthorization
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationFilter, CustomAuthorizeAttribute>();
            return services;
        }
    }
}
