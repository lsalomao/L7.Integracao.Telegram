using L7.Integracao.Domain.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace L7.Integracao.Domain.Service
{
    public class TelegramServices : ITelegramServices
    {
        ILogger<TelegramServices> _logger { get; }
        public TelegramBotClient telegramBot { get; set; }
        public string BotKey { get; set; }

        public TelegramServices(ILogger<TelegramServices> logger)
        {
            _logger = logger;
            BotKey = Configuration.Controle.BotKey;
            telegramBot = new TelegramBotClient(BotKey);
        }

        public void Execute(string mensagem)
        {
            _logger.LogInformation("Enviar mensage");


           // var order = JsonConvert.DeserializeObject<Order>(mensagem);




            var msg = telegramBot.SendTextMessageAsync(
                                     chatId: "865697320",
                                     text: mensagem,
                                     replyMarkup: new ReplyKeyboardRemove()
                               ).Result;

        }


    }
}
