using AppCore.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using AppCore.Infrastructure.Common;
using Quartz;
using Microsoft.Extensions.Logging;

namespace AppCore.Infrastructure.Jobs
{
    public class CleanLogJob : IJob
    {
        private readonly IOptions<CleanLogSetting> _cleanLogSettings;
        public CleanLogJob(IOptions<CleanLogSetting> cleanLogSettings
        )
        {
            _cleanLogSettings = cleanLogSettings;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            LogHelper.Logger.LogInformation($"Clean Log Job start at: {DateTime.UtcNow.ToString()}");
            CleanOldLogFiles(_cleanLogSettings.Value.GetLogDirectory(),
                    _cleanLogSettings.Value.GetMaxAgeInDays());
            LogHelper.Logger.LogInformation($"Clean Log Job end at: {DateTime.UtcNow.ToString()}");
        }
        private void CleanOldLogFiles(string logDirectory, int maxAgeInDays)
        {
            try
            {
                // Get all log files in the directory
                string[] logFiles = Directory.GetFiles(logDirectory, "*.txt");
                if(logFiles != null)
                {
                    // Iterate through each log file
                    foreach (string logFile in logFiles)
                    {
                        // Get creation time of the log file
                        DateTime creationTime = File.GetCreationTime(logFile);

                        // Calculate age of the log file
                        TimeSpan age = DateTime.Now - creationTime;

                        // If log file is older than the maximum age, delete it
                        if (age.TotalDays > maxAgeInDays)
                        {
                            File.Delete(logFile);
                            LogHelper.Logger.LogInformation($"Deleted log file: {logFile}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Logger.LogError($"Error cleaning old log files: {ex.Message}");
            }
        }
    }
}
