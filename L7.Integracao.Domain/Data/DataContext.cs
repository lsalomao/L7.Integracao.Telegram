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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(p => p.DataCadastro).HasColumnType("datetime").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Cliente>().Property(p => p.DataCadastro).HasColumnType("datetime").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Usuario>().Property(p => p.DataCadastro).HasColumnType("datetime").HasDefaultValueSql("getdate()");
        }
    }
}
