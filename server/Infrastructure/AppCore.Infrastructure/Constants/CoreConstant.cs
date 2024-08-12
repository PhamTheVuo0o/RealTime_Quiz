namespace AppCore.Infrastructure.Common.Constants
{
    public static class CoreConstant
    {
        #region ServiceUpdateTime
        public const string GATEWAY_UPDATE_TIME = "2024/05/07 14:54:00";
        public const string CORE_UPDATE_TIME = "2024/05/07 14:54:00";
        public const string IDENTITY_UPDATE_TIME = "2024/05/10 10:26:00";
        #endregion
        #region Env Name
        public const string DOCKER_DEV_ENV = "DockerDev";
        public const string DOCKER_LOCAL_ENV = "DockerLocal";
        public const string JWT_SECRET_64_SYMBOL = "JWT_SECRET_64_SYMBOL";
        public const string ALLOWED_HOSTS = "ALLOWED_HOSTS";
        public const string TOKEN_LIFE_TIME_MINUTES = "TOKEN_LIFE_TIME_MINUTES";
        public const string OTP_TOKEN_EXPIRED_TIME_MINUTES = "OTP_TOKEN_EXPIRED_TIME_MINUTES";
        public const string PERMANENT_TOKEN_LIFE_TIME_DAYS = "PERMANENT_TOKEN_LIFE_TIME_DAYS";
        public const string CONNECTION_STRINGS = "CONNECTION_STRINGS";
        public const string BASE_APPCORE_DOMAIN = "BASE_APPCORE_DOMAIN";
        public const string HANGFIRE_USERNAME = "HANGFIRE_USERNAME";
        public const string HANGFIRE_PASSWORD = "HANGFIRE_PASSWORD";
        #endregion
        #region Claim
        public const string CLAIM_ORGANIZATION_ID = "organizationId";
        public const string CLAIM_PERMISSIONS = "permissions";
        public const string CLAIM_AVATAR = "avatar";
        public const string CLAIM_ORGANIZATION = "organization";
        public const string CLAIM_LASTNAME = "lastName";
        public const string CLAIM_FIRSTNAME = "firstName";
        public const string CLAIM_ISINTERNAL = "isinternal";
        #endregion
        #region Core Constants
        public const string PASSWORD_DEFAULT = "Def@ultP@ss9";//A@AppCore.com

        #endregion Core Constants
        #region Formats

        public const string DEFAULT_DECIMAL_FORMAT = "0.###";
        public const string FORMAT_HEXA_STRING = "X2";

        #endregion Formats
        #region Regular Expressions

        public const string REGEX_EMAIL = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string REGEX_PHONE_NUMBER = @"^\(?(([0-9]|\+)[0-9]{2})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4,7})$";
        public const string REGEX_VERSION = @"^(\d.){3}\d$";

        #endregion Regular Expressions
    }
}
