using L7.Integracao.Domain.Extensoes;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Service;
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

            //if (userToIdentify == null && message.Entities.IsNotNullAndIsNotEmpty())
            //{
            //    (MessageEntity mentionEntity, string mentionText) = message.GetFirstEntityOf(it => it.Type == Telegram.Bot.Types.Enums.MessageEntityType.Mention);
            //    if (mentionEntity != null)
            //    {
            //        //this.botClient.GetChatMemberAsync(message.Chat.Id, )
            //    }
            //}

            if (userToIdentify == null)
            {
                userToIdentify = message.From;
            }

            if (userToIdentify != null)
            {
                ApiServices apiServices = new ApiServices();

                var mensagem = message.Text.Split('|')[1];

                var resultado = apiServices.PublicarOrder(new Order() { Descricao = mensagem });

                string messageText = Newtonsoft.Json.JsonConvert.SerializeObject(resultado, Newtonsoft.Json.Formatting.Indented);

                this.botClient.SendTextMessageAsync(message.Chat.Id, $"<pre>{messageText}</pre>",
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                    replyToMessageId: message.MessageId
                ).Sync();
            }
        }
    }
}
