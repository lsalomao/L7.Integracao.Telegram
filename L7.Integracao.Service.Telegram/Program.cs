using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Repository;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

                    //services.AddSingleton<IApiServices, ApiServices>();
                    // services.AddScoped<IRepository, ModelRespotory>();
                    services.AddScoped<IConfiguracaoMsgRepository, ConfiguracaoMsgRepository>();


                    services.AddHostedService<Worker>();
                });
    }
}
