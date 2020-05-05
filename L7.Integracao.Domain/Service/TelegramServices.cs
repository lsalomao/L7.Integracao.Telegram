using L7.Integracao.Domain.Extensoes;
using L7.Integracao.Domain.Repository.Commands;
using L7.Integracao.Domain.Service.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace L7.Integracao.Domain.Service
{
    public class TelegramServices : ITelegramServices
    {
        ILogger<TelegramServices> logger { get; }
        private readonly TelegramBotClient botClient;
        private readonly User me;
        private readonly IEnumerable<ICommand> commands;

        public IEnumerable<UpdateType> RequiredUpdates => new UpdateType[] { UpdateType.Message };

        public TelegramServices(ILogger<TelegramServices> logger, TelegramBotClient botClient, User user, IEnumerable<ICommand> commands)
        {
            this.logger = logger;
            this.botClient = botClient;
            this.me = user;
            this.commands = commands;
        }

        public async Task Inicializar(CancellationToken cancellationToken)
        {
            if (this.commands.Any())
            {
                await this.botClient.SetMyCommandsAsync(this.commands.Select(it => new BotCommand() { Command = it.Command, Description = it.Description }), cancellationToken);
            }
            else
            {
                await this.botClient.SetMyCommandsAsync(new[] { new BotCommand() { Command = "/hello", Description = "Hello World" } }, cancellationToken);
            }

            botClient.OnMessage += BotClient_OnMessage;
        }

        private void BotClient_OnMessage(object sender, MessageEventArgs e)
        {
            (MessageEntity firstMessageEntity, string commandText) = e.Message.GetFirstEntityOf(it => it.Type == MessageEntityType.BotCommand);

            if (e.Message.Type == MessageType.Text)
            {
                ICommand command = this.commands.FirstOrDefault(it => it.Command == commandText || $"{it.Command}@{this.me.Username}" == commandText);

                if (command != null)
                {
                    command.Handle(e.Message);
                }
                else
                {
                    this.botClient.SendTextMessageAsync(e.Message.Chat.Id, $"<pre>Opção inválida, por favor tenta novamente</pre>",
                            replyToMessageId: e.Message.MessageId).Sync();
                }

                this.logger.LogInformation("Command: {time}", e.Message.Entities.First().ToString());
            }
        }

        public Task Stop(CancellationToken cancellationToken) => Task.CompletedTask;



        //public void Execute(string mensagem)
        //{
        //    _logger.LogInformation("Enviar mensage");


        //   // var order = JsonConvert.DeserializeObject<Order>(mensagem);




        //    var msg = telegramBot.SendTextMessageAsync(
        //                             chatId: "865697320",
        //                             text: mensagem,
        //                             replyMarkup: new ReplyKeyboardRemove()
        //                       ).Result;

        //}


    }
}
