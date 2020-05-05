using L7.Integracao.Domain.Service;
using L7.Integracao.Domain.Service.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace L7.Integracao.Service.Controlador
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceL7 receiverServices;

        public Worker(ILogger<Worker> logger, IServiceL7 receiverServices)
        {
            _logger = logger;
            this.receiverServices = receiverServices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    Task task = Task.Run(async () => { await receiverServices.Execute(); });
                    task.Wait();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);

                    await Task.Delay(TimeSpan.FromSeconds(Configuration.Controle.TempoIntervaloControleEmSegundos), stoppingToken);
                }
            }
        }
    }
}
