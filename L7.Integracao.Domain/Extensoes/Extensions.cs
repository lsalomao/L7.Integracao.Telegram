using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace L7.Integracao.Domain.Extensoes
{
    public static class Extensions
    {

        /// <summary>
        /// Register a type as itself and your interface
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        public static void AddSingleton2<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
        {
            services.AddSingleton<TImplementation>();

            services.AddSingleton<TInterface>(sp => sp.GetRequiredService<TImplementation>());
        }


        public static T Sync<T>(this Task<T> task) => task.GetAwaiter().GetResult();

        public static bool IsNotNullAndIsNotEmpty<T>(this IEnumerable<T> list) => list != null && list.Any();


        public static (MessageEntity, string) GetFirstEntityOf(this Message message, Func<MessageEntity, bool> where)
        {
            if (message.Entities.IsNotNullAndIsNotEmpty())
            {
                for (var i = 0; i < message.Entities.Length; i++)
                {
                    if (where(message.Entities[i]))
                    {
                        return (
                                    message.Entities[i],
                                    message.EntityValues.ToArray()[i]
                                );
                    }
                }
            }
            return (null, null);
        }

        public static IServiceCollection AddTransientWithRetry<TService, TException>(
              this IServiceCollection services,
              Func<IServiceProvider, TService> implementationFactory,
              int retryCount = 3
          )
      where TService : class
      where TException : Exception
        {
            services.AddTransient<TService>(sp =>
            {

                TService service = default(TService);

                var policy = Policy
                  .Handle<TException>()
                  .WaitAndRetry(retryCount, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                  );

                policy.Execute(() =>
                {
                    service = implementationFactory(sp);
                });

                return service;

            });

            return services;
        }

    }
}
