using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Extensoes;
using L7.Integracao.Domain.Repository;
using L7.Integracao.Domain.Repository.Commands;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service;
using L7.Integracao.Domain.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace L7.Integracao.Service.Telegram
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.DataBase.ConnectionStringDefault));

                    services.AddSingleton(sp => new TelegramBotClient(Configuration.Controle.BotKey));

                    services.AddSingleton(sp => sp.GetRequiredService<TelegramBotClient>().GetMeAsync().Sync());
                                        
                    services.AddSingleton2<ITelegramServices, TelegramServices>();
                                        
                    services.AddSingleton<ICommand, OrderCommand>();

                    //services.AddScoped<IConfiguracaoMsgRepository, ConfiguracaoMsgRepository>();


                    services.AddHostedService<Worker>();
                });
    }
}
