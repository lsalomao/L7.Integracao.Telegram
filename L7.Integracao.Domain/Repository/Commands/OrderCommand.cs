using L7.Integracao.Domain.Extensoes;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Service;
using L7.Integracao.Domain.ValueObjetc;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace L7.Integracao.Domain.Repository.Commands
{
    public class OrderCommand : ICommand
    {
        private readonly TelegramBotClient botClient;

        public OrderCommand(TelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public string Command => "/order";

        public string Description => "Criar uma order";

        public void Handle(Message message)
        {
            User userToIdentify = message.ReplyToMessage?.From;

            if (userToIdentify == null)
            {
                userToIdentify = message.From;
            }

            if (userToIdentify != null)
            {
                ApiServices apiServices = new ApiServices();

                var mensagem = message.Text.Split('|')[1];

                var resultado = apiServices.PublicarOrder(new OrderVO() { Descricao = mensagem, IdTelegram = Convert.ToString(message.From.Id) });

                this.botClient.SendTextMessageAsync(message.Chat.Id, $"<pre> Order Criada com sucesso. Número: {resultado.Id}</pre>",
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                    replyToMessageId: message.MessageId
                ).Sync();
            }
        }
    }
}
