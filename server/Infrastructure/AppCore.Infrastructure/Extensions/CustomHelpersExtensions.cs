using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Infrastructure.Extensions
{
    public static class CustomHelpersExtensions
    {
        public static int GetIntDefaultOrFromEnvValue(this int? value, string evnName, int defaultValue=0)
        {
            int? envValue = Environment.GetEnvironmentVariable(evnName)?.ToInt(0);
            return value != null ? value.Value : (envValue != null ? envValue.Value : defaultValue);
        }
        public static short ToShort<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            return Convert.ToInt16(enumValue);
        }
        public static bool IsInEnum<TEnum>(this short value) where TEnum : Enum
        {
            return Enum.IsDefined(typeof(TEnum), (int)value);
        }
        public static DateTime? RemoveMilliseconds(this DateTime? dateTime)
        {
            if(dateTime == null)
                return null;
            return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day,
                dateTime.Value.Hour, dateTime.Value.Minute, dateTime.Value.Second, 0, dateTime.Value.Kind);
        }

        public static string ToRankTopic(this Guid value)
        {
            return $"{value.ToString()}_Rank";
        }
    }
}
