using L7.Integracao.Domain.Application;
using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Extensoes;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service;
using L7.Integracao.Domain.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using Telegram.Bot;

namespace L7.Integracao.Service.Controlador
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.DataBase.ConnectionStringDefault), ServiceLifetime.Singleton);

                    services.AddTransient<IConfiguracaoMsgRepository, ConfiguracaoMsgRepository>();
                    services.AddTransient(sp => sp.GetRequiredService<IConfiguracaoMsgRepository>().GetFirst());

                    services.AddTransient(sp => new ConnectionFactory()
                    {
                        HostName = sp.GetRequiredService<ConfiguracaoMsg>().UrlMsg,
                        Port = sp.GetRequiredService<ConfiguracaoMsg>().Porta,
                        UserName = sp.GetRequiredService<ConfiguracaoMsg>().Login,
                        Password = sp.GetRequiredService<ConfiguracaoMsg>().Senha
                    });

                    services.AddTransientWithRetry<IConnection, BrokerUnreachableException>((sp) => sp.GetRequiredService<ConnectionFactory>().CreateConnection(), 3);

                    services.AddTransient(sp => sp.GetRequiredService<IConnection>().CreateModel());

                    services.AddTransient<IConsumerServices<Order>>(sp => new ReliableConsumerServices<Order>(sp.GetRequiredService<IModel>(), 3));

                    services.AddTransient(sp => new TelegramBotClient(Configuration.Controle.BotKey));

                    services.AddTransient(sp => sp.GetRequiredService<TelegramBotClient>().GetMeAsync().Sync());

                    services.AddTransient<ITelegramConsumerServices, TelegramConsumerServices>();

                    services.AddTransient<OrderConsumerServices>();

                    services.AddHostedService<Worker>();
                });
    }
}
