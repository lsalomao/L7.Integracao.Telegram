using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L7.Integracao.WebApi
{
    public class Configuration
    {
        private static readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
                                                     .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                     .Build();

        public class Controle
        {
            public static int TempoIntervaloControleEmMinutos { get { return Convert.ToInt32(_configuration["AppSettings:Controle:TempoIntervaloControleEmMinutos"]); } }
            public static int TempoIntervaloControleEmSegundos { get { return Convert.ToInt32(_configuration["AppSettings:Controle:TempoIntervaloControleEmSegundos"]); } }            
        }

        public class DataBase
        {
            public static string ConnectionStringDefault { get { return _configuration["L7Helper:ConnectionStrings:Default:ConnectionString"]; } }
        }
    }
}
