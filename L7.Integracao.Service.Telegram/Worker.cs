using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Service;
using L7.Integracao.Service.Telegram.Controller;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace L7.Integracao.Service.Telegram
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        int count = 0;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                    TelegramController telegramController = new TelegramController();

                    Task task = Task.Run(telegramController.Iniciar);
                    task.Wait();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(Configuration.Controle.TempoIntervaloControleEmSegundos), stoppingToken);
                }
            }
        }
    }
}
