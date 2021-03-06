﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace L7.Integracao.Domain.Model
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        public string MensagemRetorno { get; set; }
        public DateTime DataCadastro { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; }
    }
}
