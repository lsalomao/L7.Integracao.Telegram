using System;
using System.Collections.Generic;
using System.Text;

namespace L7.Integracao.Domain.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }        
        public DateTime DataCadastro { get; set; }
    }
}
