using MediatR;
using MediatR.Pipeline;
using AppCore.Core.API.Application.Behavior;
using System.Reflection;

namespace AppCore.Core.API.Services
{
    public static class PipelineBehaviorService
    {
        public static void RegisterPipelineBehaviors(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        }
    }
}
