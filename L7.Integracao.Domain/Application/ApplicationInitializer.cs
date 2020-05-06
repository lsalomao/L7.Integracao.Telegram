using L7.Integracao.Domain.Model;
using RabbitMQ.Client;

namespace L7.Integracao.Domain.Application
{
    public class ApplicationInitializer
    {
        private readonly IModel rabbitMQModel;
        private readonly ConfiguracaoMsg configuracaoMsg;

        public ApplicationInitializer(ConfiguracaoMsg configuracaoMsg, IModel rabbitMQModel)
        {
            this.rabbitMQModel = rabbitMQModel;
            this.configuracaoMsg = configuracaoMsg;
        }

        public void Initializer()
        {
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            rabbitMQModel.QueueDeclare(queue: configuracaoMsg.NomeFila, durable: true, exclusive: false, autoDelete: false, arguments: null);

            rabbitMQModel.ExchangeDeclare(exchange: configuracaoMsg.NomeTopico, ExchangeType.Topic, durable: true, arguments: null);

            rabbitMQModel.QueueBind(queue: configuracaoMsg.NomeFila, exchange: configuracaoMsg.NomeTopico, routingKey: string.Empty);
        }
    }
}
