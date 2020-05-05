using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Service
{
    public class SenderServices : ISenderServices
    {
        private readonly IConfiguracaoMsgRepository _repository;
        private readonly ILogger<SenderServices> _logger;
        ConfiguracaoMsg _configuracaoMsg;

        public SenderServices(IConfiguracaoMsgRepository repository, ILogger<SenderServices> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public void Execute(Order order)
        {
            try
            {
                _configuracaoMsg = _repository.GetFirst();

                var _connectionFactory = new ConnectionFactory()
                {
                    HostName = _configuracaoMsg.UrlMsg,
                    UserName = _configuracaoMsg.Login,
                    Password = _configuracaoMsg.Senha
                };

                using var conexao = _connectionFactory.CreateConnection();
                using var canal = conexao.CreateModel();

                canal.QueueDeclare(queue: _configuracaoMsg.NomeFila, durable: true, exclusive: false, autoDelete: false, arguments: null);
                canal.ExchangeDeclare(exchange: _configuracaoMsg.NomeTopico, type: ExchangeType.Topic, durable: true, arguments: null);
                canal.QueueBind(queue: _configuracaoMsg.NomeFila, exchange: _configuracaoMsg.NomeTopico, routingKey: string.Empty);


                //canal.QueueDeclare(queue: configuracao.NomeFila, durable: true, exclusive: false, autoDelete: false, arguments: null);
                //canal.ExchangeDeclare(exchange: configuracao.NomeTopico, type: ExchangeType.Topic, durable: true, arguments: null);
                //canal.QueueBind(queue: configuracao.NomeFila, exchange: configuracao.NomeTopico, routingKey: string.Empty);


                byte[] bufferPayload = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(order));

                canal.BasicPublish(exchange: _configuracaoMsg.NomeTopico, routingKey: string.Empty, basicProperties: null, body: bufferPayload);

                _logger.LogInformation($"Order id: {order.Id} publicada.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }
}
