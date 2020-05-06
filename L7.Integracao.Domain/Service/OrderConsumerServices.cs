using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace L7.Integracao.Domain.Service
{
    public class OrderConsumerServices
    {
        private readonly IConsumerServices<Order> consumer;
        private readonly ITelegramConsumerServices telegramConsumer;
        private readonly IModelRepository<Order> modelRepository;

        public OrderConsumerServices(IConsumerServices<Order> consumer, ITelegramConsumerServices telegramConsumer, IModelRepository<Order> modelRepository)
        {
            this.consumer = consumer;
            this.telegramConsumer = telegramConsumer;
            this.modelRepository = modelRepository;
        }

        public void Consume()
        {
            this.consumer.Start("L7.Order", this.ProcessOrder);
        }

        public void ProcessOrder(Order order)
        {
            order.MensagemRetorno = $"Mensagem retorno {order.Descricao} - {order.Id} - {order.ClienteId}";

            var novo = modelRepository.GetById(order.Id);


            string mensagem = $"Segue o retorno da sua order {order.MensagemRetorno}";


            telegramConsumer.EnviarMensagem(mensagem, order.Cliente.IdTelegram);
        }
    }
}
