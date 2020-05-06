using L7.Integracao.Domain.Service;
using L7.Integracao.Domain.Service.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace L7.Integracao.Service.Controlador
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly OrderConsumerServices orderConsumer;
        private readonly TelegramBotClient botClient;

        public Worker(ILogger<Worker> logger, OrderConsumerServices orderConsumer, TelegramBotClient botClient)
        {
            _logger = logger;
            this.orderConsumer = orderConsumer;
            this.botClient = botClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    botClient.StartReceiving();

                    orderConsumer.Consume();

                    await this.MainThread(stoppingToken);

                    this.botClient.StopReceiving();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);

                    await Task.Delay(TimeSpan.FromSeconds(Configuration.Controle.TempoIntervaloControleEmSegundos), stoppingToken);
                }
            }
        }

        private async Task MainThread(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(500, cancellationToken);
            }
        }
    }
}
