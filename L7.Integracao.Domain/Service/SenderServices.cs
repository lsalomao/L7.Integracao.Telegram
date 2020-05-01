using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
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
