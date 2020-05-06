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
        private readonly ILogger<SenderServices> _logger;
        private readonly IModel rabbitMQModel;
        private readonly ConfiguracaoMsg configuracaoMsg;

        public SenderServices(ILogger<SenderServices> logger, ConfiguracaoMsg configuracaoMsg, IModel rabbitMQModel)
        {
            _logger = logger;
            this.rabbitMQModel = rabbitMQModel;
            this.configuracaoMsg = configuracaoMsg;
        }


        public void Execute(object notification)
        {
            try
            {
                byte[] bufferPayload = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(notification, Formatting.Indented));

                var prop = rabbitMQModel.CreateBasicProperties();

                rabbitMQModel.BasicPublish(exchange: configuracaoMsg.NomeTopico, routingKey: string.Empty, basicProperties: prop, body: bufferPayload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }
}
