using Lr15.Net.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lr15.Net.Services.BackgroundServices
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationBackgroundService> _logger;

        public NotificationBackgroundService(IHubContext<NotificationHub> hubContext, ILogger<NotificationBackgroundService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Send a notification message every 10 seconds
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", "New notification at " + DateTime.Now);
                    _logger.LogInformation("Sent notification to clients.");
                    await Task.Delay(10000, stoppingToken); // Wait for 10 seconds
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending notification");
                }
            }
        }
    }
}
