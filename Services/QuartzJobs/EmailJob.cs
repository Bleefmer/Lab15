using Quartz;
using System;
using System.Threading.Tasks;

namespace Lr15.Net.Services.QuartzJobs
{
    public class EmailJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Email sent at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
