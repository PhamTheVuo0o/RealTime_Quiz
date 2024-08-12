namespace AppCore.Infrastructure.Extensions
{
    public static class StringHelpersExtensions
    {
        public static string GetValueOrDefault(this string? value, string defaultValue)
        {
            return String.IsNullOrEmpty(value) ? defaultValue : value;
        }
        public static bool IsContain(this string? value, string compareValue)
        {
            if (string.IsNullOrEmpty(compareValue)) return false;
            if (string.IsNullOrEmpty(value)) return false;

            return compareValue.Contains(value);
        }
        public static string GetStringDefaultOrFromEnvValue(this string? value, string evnName, string defaultValue="")
        {
            string? envValue = Environment.GetEnvironmentVariable(evnName);
            return String.IsNullOrEmpty(value) ? (string.IsNullOrEmpty(envValue) ? defaultValue : envValue) : value;
        }
        public static List<string> GetListStringDefaultOrFromEnvValue(this List<string>? value, string evnName, string defaultValue = "", char splitChar = ',')
        {
            string? envValue = Environment.GetEnvironmentVariable(evnName);
            return value == null ? (string.IsNullOrEmpty(envValue) ? defaultValue.Split(splitChar).ToList() : envValue.Split(splitChar).ToList()) : value;
        }
        public static string GetNextIndex(this string input)
        {
            if (string.IsNullOrEmpty(input)) return "A";
            List<char> charList = new List<char>(input.ToCharArray());
            int index = charList.Count - 1;
            while (index >= 0)
            {
                if (charList[index] == 'Z')
                {
                    charList[index] = 'A';
                    index--;
                }
                else
                {
                    charList[index]++;
                    break;
                }
            }
            if (index < 0)
            {
                charList.Insert(0, 'A');
            }
            return new string(charList.ToArray());
        }
        public static int ToInt(this string value, int _default = 0)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return _default;
            }
        }

        public static long ToLong(this string value, long _default = 0)
        {
            try
            {
                return long.Parse(value);
            }
            catch
            {
                return _default;
            }
        }

        public static DateTime? DateTimeFromUnixTimeSeconds(this string value)
        {
            try
            {
                long unixTimestamp = value.ToLong();
                if (unixTimestamp == 0)
                    return null;

                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
                return dateTimeOffset.UtcDateTime;
            }
            catch
            {
                return null;
            }
        }
        public static string[] SplitAndRemoveEmpty(this string value, char[] separators)
        {
            return value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool EqualsIgnoreCase(this string firstString, string secondString)
        {
            return firstString.Equals(secondString, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string NormalizeUrl(this string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var urlNormalized = !url.EndsWith('/') ? url : url.TrimEnd('/');

                return urlNormalized;
            }
            return "";
        }

        public static Guid ToGuid(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                return Guid.Parse(value);
            else 
                return Guid.Empty;
        }
    }
}
