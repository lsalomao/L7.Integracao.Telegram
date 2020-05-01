using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Repository;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace L7.Integracao.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddEntityFrameworkSqlServer().AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.DataBase.ConnectionStringDefault));
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.DataBase.ConnectionStringDefault));

            services.AddScoped<IRepository, ModelRespotory>();
            services.AddScoped<ISenderServices, SenderServices>();
            services.AddScoped<IConfiguracaoMsgRepository, ConfiguracaoMsgRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
