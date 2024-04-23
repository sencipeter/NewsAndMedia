using NewsAndMedia.Core.Interfaces;

namespace NewsAndMedia.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageClient _messageClient;


        public Worker(ILogger<Worker> logger,
            IMessageClient messageClient)
        {
            _logger = logger;
            _messageClient = messageClient;
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _messageClient.InitClient();
            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _messageClient.ReceiveMessage();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
