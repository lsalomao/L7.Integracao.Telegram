using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L7.Integracao.Domain
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
            public static string BotKey { get { return _configuration["AppSettings:Controle:BotKey"]; } }            
        }

        public class DataBase
        {
            public static string ConnectionStringDefault { get { return _configuration["L7Helper:ConnectionStrings:Default:ConnectionString"]; } }
        }
    }
}
