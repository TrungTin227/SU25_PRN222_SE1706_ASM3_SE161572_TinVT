using SMMS.Services.TinVT;

namespace HealthCheckSession.WorkerService.TinVT
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProviders _serviceProviders;

        public Worker(ILogger<Worker> logger, IServiceProviders serviceProviders)
        {
            _logger = logger;
            _serviceProviders = serviceProviders;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    this.WriteLogHealthCheckSessionData();
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
        private async void WriteLogHealthCheckSessionData()
        {
            try
            {
                var content = string.Empty;
                var healthCheckSessions = await _serviceProviders.HealthCheckSessionTinVTService.GetAllAsync();
                if (healthCheckSessions != null && healthCheckSessions.Count > 0)
                {
                    content = Utilities.ConvertObjectToJSONString(healthCheckSessions);
                    Utilities.WriteLoggerFile(content);
                }
            }
            catch (Exception ex)
            {
                Utilities.WriteLoggerFile(ex.ToString());
            }
        }
    }
}
