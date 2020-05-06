using L7.Integracao.Domain.Service.Interface;
using Polly;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace L7.Integracao.Domain.Service
{
    public class ReliableConsumerServices<T> : ConsumerServices<T>, IConsumerServices<T>
    {
        private readonly int retryCount;
        public ReliableConsumerServices(IModel rabbitMQModel, int retryCount) : base(rabbitMQModel)
        {
            this.retryCount = retryCount;
        }

        protected override void Dispatch(T treatedObject, Action<T> action)
        {
            var policy = Policy.Handle<Exception>().WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>
            {
                action(treatedObject);
            });
        }
    }
}
