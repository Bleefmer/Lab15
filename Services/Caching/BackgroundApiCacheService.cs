using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lr15.Net.Services.Caching
{
    public class BackgroundApiCacheService : BackgroundService
    {
        private readonly ApiCacheService _apiCacheService;
        private readonly ILogger<BackgroundApiCacheService> _logger;

        public BackgroundApiCacheService(ApiCacheService apiCacheService, ILogger<BackgroundApiCacheService> logger)
        {
            _apiCacheService = apiCacheService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Fetching exchange rates...");
                    var rates = await _apiCacheService.GetRatesAsync();
                    _logger.LogInformation($"Exchange rates fetched: {rates}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error fetching exchange rates: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); // Run every 10 minutes
            }
        }
    }
}
