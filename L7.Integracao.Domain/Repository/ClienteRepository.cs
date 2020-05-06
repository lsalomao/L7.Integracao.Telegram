using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Repository
{
    public class ClienteRepository : IModelRepository<Cliente>
    {
        public DataContext _context { get; }

        public ClienteRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Cliente entity)
        {
            _context.Add(entity);
        }

        public void Delete(Cliente entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetById(int Id)
        {
            return await _context.Clientes.FirstAsync(e => e.Id.Equals(Id));
        }

        public Cliente GetByIdTelegram(string IdTelegram)
        {
            return _context.Clientes.FirstOrDefault(e => e.IdTelegram.Equals(IdTelegram));
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update(Cliente entity)
        {
            _context.Update(entity);
        }
    }
}
