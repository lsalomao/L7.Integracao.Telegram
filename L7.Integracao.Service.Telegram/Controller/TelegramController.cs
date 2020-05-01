using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace L7.Integracao.Service.Telegram.Controller
{
    public class TelegramController
    {
        public TelegramBotClient TelegramBot { get; set; }
        public string BotKey { get; set; }
        public bool IsRunning { get; set; }

        private Message lastMessage = null;

        private Boolean lockedExecution = false;

        public async Task Iniciar()
        {
            BotKey = Configuration.Controle.BotKey;
            TelegramBot = new TelegramBotClient(BotKey);

            // TelegramBot.OnUpdate += TelegramBot_OnUpdate;
            TelegramBot.OnMessage += TelegramBot_OnMessage;

            TelegramBot.StartReceiving();

            IsRunning = TelegramBot.IsReceiving;

            await Task.Delay(Timeout.Infinite);
        }

        private void TelegramBot_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                Boolean respondeu = false;
                var message = e.Message;
                string command = string.Empty;

                if (message == null)
                {
                    return;
                }
                else
                {
                    lastMessage = message;
                }


                if (lockedExecution)
                {
                    //    var msg = RespostaAcaoAjuda(TelegramBot, message);

                    var action = message.Text.Split('/')[1];


                    switch (action)
                    {
                        // COMANDO PARA INICIAR A INTERAÇÃO COM O BOT
                        case "start":
                            RespostaAcaoIniciar(TelegramBot, message);

                            break;
                        case "order":
                            RespostaAcaoCriarOrder(TelegramBot, message);

                            break;
                        case "ajuda":
                            RespostaAcaoAjuda(TelegramBot, message);
                            break;
                        default:
                            RespostaAcaoDefault(TelegramBot, message);
                            break;
                    };

                    if (action.Contains("order"))
                    {
                        RespostaAcaoCriarOrder(TelegramBot, message);
                    }

                    lockedExecution = false;
                    respondeu = true;
                    return;
                }

                lockedExecution = true;
            }
            catch (Exception ex)
            {

            }
        }

        //private void TelegramBot_OnUpdate(object sender, UpdateEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        public Message RespostaAcaoCriarOrder(TelegramBotClient telegramBot, Message message)
        {
            ApiServices apiServices = new ApiServices();

            var mensagem = message.Text.Split('|')[1];

            var resultado = apiServices.PublicarOrder(new Order() { Descricao = mensagem });


            return telegramBot.SendTextMessageAsync(
                              chatId: message.Chat.Id,
                              text: $"Ordem foi criada! Número: {resultado.Id} ",
                              replyMarkup: new ReplyKeyboardRemove()
                        ).Result;
        }

        public Message RespostaAcaoLockedExecution(TelegramBotClient telegramBot, Message message)
        {
            return telegramBot.SendTextMessageAsync(
                              chatId: message.Chat.Id,
                              text: " ainda está executando uma requisição anterior.",
                              replyMarkup: new ReplyKeyboardRemove()
                        ).Result;
        }

        public Message RespostaAcaoDefault(TelegramBotClient telegramBot, Message message)
        {
            return telegramBot.SendTextMessageAsync(
                              chatId: message.Chat.Id,
                              text: " Opção inválida, por favor tenta novamente.",
                              replyMarkup: new ReplyKeyboardRemove()
                        ).Result;
        }

        public Message RespostaAcaoIniciar(TelegramBotClient telegramBot, Message message)
        {
            return telegramBot.SendTextMessageAsync(
                             chatId: message.Chat.Id,
                             text: " Digite sua ordem ! (/order | texto).",
                             replyMarkup: new ReplyKeyboardRemove()
                       ).Result;
        }

        public Message RespostaAcaoAjuda(TelegramBotClient telegramBot, Message message)
        {
            return telegramBot.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: " Ajuda",
                            replyMarkup: new ReplyKeyboardRemove()
                      ).Result;
        }
    }
}
