using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Lr15.Net.Services.BackgroundTasks
{
    public class PageCheckerService : BackgroundService
    {
        private readonly ILogger<PageCheckerService> _logger;
        private readonly HttpClient _httpClient;

        public PageCheckerService(ILogger<PageCheckerService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var response = await _httpClient.GetAsync("https://example.com", stoppingToken);
                    var status = response.IsSuccessStatusCode ? "Available" : "Unavailable";

                    _logger.LogInformation($"[{DateTime.Now}] Website status: {status}");
                    await File.AppendAllTextAsync("Logs/PageCheckerLog.txt",
                        $"[{DateTime.Now}] Website status: {status}\n", stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error checking website status.");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}
