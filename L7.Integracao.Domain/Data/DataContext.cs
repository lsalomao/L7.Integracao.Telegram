using L7.Integracao.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace L7.Integracao.Domain.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ConfiguracaoMsg> ConfiguracoesMsg { get; set; }
    }
}
