using System;
using System.Collections.Generic;
using System.Text;

namespace L7.Integracao.Domain.Model
{
    public class ConfiguracaoMsg
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Sender { get; set; }
        public bool Receiver { get; set; }
        public string NomeTopico { get; set; }
        public string NomeFila { get; set; }
        public string UrlMsg { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public int Porta { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
