using System.Threading;
using System.Threading.Tasks;

namespace SchedulingGenerate.Services.BackgroundService
{
    public class LongRunningService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly BackgroundWorkerQueue queue;

        public LongRunningService(BackgroundWorkerQueue queue)
        {
            this.queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await queue.DequeueAsync(stoppingToken);

                await workItem(stoppingToken);
            }
        }
    }
}