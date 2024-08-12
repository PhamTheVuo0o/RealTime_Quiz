using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AppCore.Infrastructure.Persistence.Constant;
using AppCore.Infrastructure.Common;
using AppCore.Infrastructure.Extensions;
using AppCore.Infrastructure.Common.Constants;

namespace AppCore.Infrastructure.Persistence.Services
{
    public static class ConfigureDatabase
    {
        public static DbContextOptionsBuilder AddCustomNpgsqlDbOptions(this DbContextOptionsBuilder options, IConfiguration config)
        {
            var appSetting = new AppSetting();
            config.GetSection("AppSetting").Bind(appSetting);

            options.UseNpgsql(appSetting.GetDefaultConnection,
            npgsqlOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(maxRetryCount: CoreDataAccessConstant.DEFAULT_DB_RETRY_ON_FAILURE,
                    maxRetryDelay: TimeSpan.FromSeconds(CoreDataAccessConstant.DEFAULT_DB_RETRY_ON_FAILURE_DELAY_IN_SECONDS),
                    errorCodesToAdd: null);
            });

            return options;
        }
    }
}
