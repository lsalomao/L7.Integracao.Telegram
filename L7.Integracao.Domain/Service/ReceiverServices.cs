using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service.Interface;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Service
{
    public class ReceiverServices : IServiceL7
    {
        ILogger<ReceiverServices> _logger { get; }
        IConfiguracaoMsgRepository _repository { get; }
        ITelegramServices _telegramServices { get; }
        ConfiguracaoMsg _configuracaoMsg;

        public ReceiverServices(IConfiguracaoMsgRepository repository, ITelegramServices telegramServices, ILogger<ReceiverServices> logger)
        {
            _repository = repository;
            _logger = logger;
            _telegramServices = telegramServices;
        }


        public async Task Execute()
        {
            _logger.LogInformation("Chegamos.");

            _configuracaoMsg = _repository.GetFirst();

            try
            {
                var _connectionFactory = new ConnectionFactory()
                {
                    HostName = _configuracaoMsg.UrlMsg,
                    UserName = _configuracaoMsg.Login,
                    Password = _configuracaoMsg.Senha
                };

                using var conexao = _connectionFactory.CreateConnection();
                using var canal = conexao.CreateModel();

                var consumer = new EventingBasicConsumer(canal);
                canal.BasicConsume(queue: _configuracaoMsg.NomeFila, autoAck: false, consumer: consumer);

                consumer.Received += (model, ea) =>
                {
                    _logger.LogInformation($"Nova mensagem...");

                    try
                    {
                        var obj = Encoding.ASCII.GetString(ea.Body.ToArray());

                        _logger.LogInformation($"Mensagem Recebida - {obj}");

                        //var mensagemRecebida = JsonConvert.DeserializeObject(obj);

                        if (obj != null)
                        {
                           // _telegramServices.Execute(obj);

                            canal.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                        else
                        {
                            canal.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                        }
                    }
                    catch (Exception ex)
                    {
                        canal.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                        throw ex;
                    }

                };

                _logger.LogInformation($"Aguardando por mensagens...");

                await Task.Delay(Timeout.Infinite);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro");
                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }
    }
}
