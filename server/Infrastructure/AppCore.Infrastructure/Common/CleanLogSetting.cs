using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Infrastructure.Common
{
    public class CleanLogSetting
    {
        public string? LogFilePath { get; set; }
        public string? CronSchedule { get; set; }
        public int MaxAgeInDays { get; set; }
        public string GetLogDirectory()
        {
            if (!string.IsNullOrWhiteSpace(LogFilePath))
            {
                return Path.GetDirectoryName(LogFilePath);
            }
            return "Logs";
        }
        public string GetCronSchedule()
        {
            if (!string.IsNullOrWhiteSpace(CronSchedule))
            {
                return CronSchedule;
            }
            return "0 0 0 ? * *";
        }
        public int GetMaxAgeInDays()
        {
            return MaxAgeInDays>1 ? MaxAgeInDays:1;
        }
        public CleanLogSetting()
        {
            MaxAgeInDays = 7;
            LogFilePath = "Logs";
            CronSchedule = "0 0 0 ? * *";
        }
    }
}
