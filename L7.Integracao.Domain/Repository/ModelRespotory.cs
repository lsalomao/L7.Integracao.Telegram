using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Repository
{
    public class ModelRespotory : IRepository
    {
        public DataContext _context { get; }

        public ModelRespotory(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity)
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity)
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }


        public async Task<IEnumerable> ListarOrder()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> ListarOrderById(int Id)
        {
            return await _context.Orders.FirstAsync(e => e.Id.Equals(Id));
        }
    }
}
