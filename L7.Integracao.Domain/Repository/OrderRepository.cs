using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Repository
{
    public class OrderRepository : IModelRepository<Order>
    {
        public DataContext _context { get; }

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Order entity)
        {
            _context.Add(entity);
        }

        public void Delete(Order entity)
        {
            _context.Remove(entity);
        }
        public void Update(Order entity)
        {
            _context.Update(entity);
        }

        public async Task<IEnumerable> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetById(int Id)
        {
            return await _context.Orders.FirstAsync(e => e.Id.Equals(Id));
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
