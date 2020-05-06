using L7.Integracao.Domain.Service.Interface;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace L7.Integracao.Domain.Service
{
    public class ConsumerServices<T> : IConsumerServices<T>
    {
        private readonly IModel rabbitMQModel;

        public ConsumerServices(IModel rabbitMQModel)
        {
            this.rabbitMQModel = rabbitMQModel;
        }

        public void Start(string nomeFila, Action<T> action)
        {
            var consumer = new EventingBasicConsumer(rabbitMQModel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.ASCII.GetString(ea.Body.ToArray());

                T treatedObject = default(T);

                try
                {
                    treatedObject = JsonConvert.DeserializeObject<T>(message);

                }
                catch (Exception ex)
                {
                    rabbitMQModel.BasicReject(ea.DeliveryTag, true);
                    throw;
                }

                try
                {
                    Dispatch(treatedObject, action);
                    rabbitMQModel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception)
                {
                    rabbitMQModel.BasicNack(ea.DeliveryTag, false, true);
                    throw;
                }



            };

            rabbitMQModel.BasicConsume(queue: nomeFila, autoAck: false, consumer: consumer);
        }

        protected virtual void Dispatch(T treatedObject, Action<T> action) => action(treatedObject);
    }
}
