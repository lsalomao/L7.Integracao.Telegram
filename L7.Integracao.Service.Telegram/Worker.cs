using L7.Integracao.Domain.Service.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;


namespace L7.Integracao.Service.Telegram
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly TelegramBotClient botClient;
        private readonly ITelegramServices telegramServices;

        public Worker(ILogger<Worker> logger, TelegramBotClient botClient, ITelegramServices telegramServices)
        {
            _logger = logger;
            this.botClient = botClient;
            this.telegramServices = telegramServices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await telegramServices.Inicializar(stoppingToken);

            UpdateType[] updateTypes = telegramServices.RequiredUpdates.ToArray();

            this.botClient.StartReceiving(allowedUpdates: updateTypes, cancellationToken: stoppingToken);
            

            await this.MainThread(stoppingToken);

            await telegramServices.Stop(stoppingToken);

            this.botClient.StopReceiving();
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
