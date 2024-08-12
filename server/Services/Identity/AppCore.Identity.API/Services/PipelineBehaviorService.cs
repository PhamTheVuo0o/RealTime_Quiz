using MediatR;
using MediatR.Pipeline;
using AppCore.Identity.API.Application.Behavior;
using System.Reflection;

namespace AppCore.Identity.API.Services
{
    public static class PipelineBehaviorService
    {
        public static void RegisterPipelineBehaviors(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        }
    }
}
