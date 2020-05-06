using L7.Integracao.Domain.Extensoes;
using L7.Integracao.Domain.Service.Interface;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace L7.Integracao.Domain.Service
{
    public class TelegramConsumerServices : ITelegramConsumerServices
    {
        private readonly TelegramBotClient botClient;

        public TelegramConsumerServices(TelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public void EnviarMensagem(object mensagem, string cliente)
        {
            this.botClient.SendTextMessageAsync(cliente, $"<pre>{mensagem}</pre>", ParseMode.Html).Sync();
        }
    }
}
