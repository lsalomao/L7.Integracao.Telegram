using L7.Integracao.Domain.Application;
using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Extensoes;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service;
using L7.Integracao.Domain.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace L7.Integracao.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.DataBase.ConnectionStringDefault), ServiceLifetime.Singleton);
                        
            services.AddTransient<IConfiguracaoMsgRepository, ConfiguracaoMsgRepository>();

            services.AddTransient(sp => sp.GetRequiredService<IConfiguracaoMsgRepository>().GetFirst());
            InitRabbitMQ(services);
            services.AddSingleton<ApplicationInitializer>();

            services.AddTransient<IModelRepository<Order>, OrderRepository>();
            services.AddTransient<IModelRepository<Cliente>, ClienteRepository>();

            services.AddTransient<IOrderServices, OrderServices>();


            services.AddTransient<ISenderServices, SenderServices>();            

            services.AddMvc();
        }

        private void InitRabbitMQ(IServiceCollection services)
        {
            services.AddTransient(sp => new ConnectionFactory()
            {
                HostName = sp.GetRequiredService<ConfiguracaoMsg>().UrlMsg,
                Port = sp.GetRequiredService<ConfiguracaoMsg>().Porta,
                UserName = sp.GetRequiredService<ConfiguracaoMsg>().Login,
                Password = sp.GetRequiredService<ConfiguracaoMsg>().Senha
            });

            services.AddTransientWithRetry<IConnection, BrokerUnreachableException>((sp) => sp.GetRequiredService<ConnectionFactory>().CreateConnection(), 3);

            services.AddTransient(sp => sp.GetRequiredService<IConnection>().CreateModel());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.GetRequiredService<ApplicationInitializer>().Initializer();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
