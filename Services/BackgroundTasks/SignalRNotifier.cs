using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Lr15.Net.Services.BackgroundTasks
{
    public class SignalRNotifier : BackgroundService
    {
        private readonly IHubContext<Hubs.NotificationHub> _hubContext;

        public SignalRNotifier(IHubContext<Hubs.NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "System", "Periodic update");
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
