using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository;
using L7.Integracao.Domain.Repository.Interfaces;
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

                    services.AddScoped<IModelRepository<Order>, OrderRepository>();

                    services.AddHostedService<Worker>();
                });
    }
}
