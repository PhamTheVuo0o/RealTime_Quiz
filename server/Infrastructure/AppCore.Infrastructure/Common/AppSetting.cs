using AppCore.Infrastructure.Common.Constants;
using AppCore.Infrastructure.Extensions;

namespace AppCore.Infrastructure.Common
{
    public class AppSetting
    {
        public List<string> AllowedHosts { get; set; }
        public int? TokenLifetimeMinutes { get; set;}
        public int? PermanentTokenLifetimeDays { get; set; }
        public string? JWTSecret64Symbol { get; set; }
        public int? OTPTokenExpiredTimeMinutes { get; set;}
        public string? LoginUrl { get;set; }
        public string DefaultAvatar { get; set; }
        public string FileLogoName { get; set; }
        public List<string> BaseAppCoreDomains { get; set; }
        public ConnectionString? ConnectionStrings { get; set; }
        public HangfireSetting? HangfireSettings { get; set; }

        public int GetTokenLifetimeMinutes => TokenLifetimeMinutes.
            GetIntDefaultOrFromEnvValue(CoreConstant.TOKEN_LIFE_TIME_MINUTES,1440);
        public int GetOTPTokenExpiredTimeMinutes => OTPTokenExpiredTimeMinutes.
            GetIntDefaultOrFromEnvValue(CoreConstant.OTP_TOKEN_EXPIRED_TIME_MINUTES,15);
        public int GetPermanentTokenLifetimeDays => PermanentTokenLifetimeDays.
            GetIntDefaultOrFromEnvValue(CoreConstant.PERMANENT_TOKEN_LIFE_TIME_DAYS,7);
        public string GetJWTSecret64Symbol => JWTSecret64Symbol.
            GetStringDefaultOrFromEnvValue(CoreConstant.JWT_SECRET_64_SYMBOL);
        public string GetDefaultConnection => ConnectionStrings?.DefaultConnection.
            GetStringDefaultOrFromEnvValue(CoreConstant.CONNECTION_STRINGS);

        public List<string> GetBaseAppCoreDomain => BaseAppCoreDomains.
            GetListStringDefaultOrFromEnvValue(CoreConstant.BASE_APPCORE_DOMAIN, "");

        public string GetHangfireUserName => HangfireSettings?.UserName.
            GetStringDefaultOrFromEnvValue(CoreConstant.HANGFIRE_USERNAME,"admin");

        public string GetHangfirePassword => HangfireSettings?.Password.
            GetStringDefaultOrFromEnvValue(CoreConstant.HANGFIRE_PASSWORD, "hfP@ssw0rd");

        public AppSetting()
        {
            AllowedHosts = new List<string>();
            BaseAppCoreDomains = new List<string>();
            TokenLifetimeMinutes = 1440;
            OTPTokenExpiredTimeMinutes = 15;
            PermanentTokenLifetimeDays = 7;
            JWTSecret64Symbol = string.Empty;
            ConnectionStrings = new ConnectionString();
            HangfireSettings = new HangfireSetting();
        }
    }
    public class ConnectionString
    {
        public string? DefaultConnection { get; set; }
    }
}
