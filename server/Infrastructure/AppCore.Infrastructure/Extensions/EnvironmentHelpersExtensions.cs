using Microsoft.Extensions.Hosting;
using AppCore.Infrastructure.Common.Constants;

namespace AppCore.Infrastructure.Extensions
{
    public static class EnvironmentHelpersExtensions
    {
        public static bool IsDevEnv(this IHostEnvironment value)
        {
            return ((value.IsDevelopment()) | (value.IsEnvironment(CoreConstant.DOCKER_DEV_ENV)) | (value.IsEnvironment(CoreConstant.DOCKER_LOCAL_ENV)));
        }
    }
}
