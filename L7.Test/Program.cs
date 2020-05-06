using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository;
using L7.Integracao.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace L7.Test
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
                    services.AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=40.117.252.103;Initial Catalog=L7_DB_ESTUDO;User Id=leandro.salomao;Password=123@mudar;Pooling=True;"), ServiceLifetime.Singleton);
                    services.AddSingleton<IModelRepository<Order>, OrderRepository>();

                    services.AddHostedService<Worker>();
                });
    }
}
