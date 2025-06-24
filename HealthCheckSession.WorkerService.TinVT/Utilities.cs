using System.Text.Json;

namespace HealthCheckSession.WorkerService.TinVT
{
    public static class Utilities
    {
        private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "healthcheck_log.txt");

        public static string ConvertObjectToJSONString(object obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(obj, options);
        }

        public static void WriteLoggerFile(string content)
        {
            try
            {
                File.AppendAllText(LogFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {content}{Environment.NewLine}");
            }
            catch
            {
                // Optionally handle file write exceptions here
            }
        }
    }
}