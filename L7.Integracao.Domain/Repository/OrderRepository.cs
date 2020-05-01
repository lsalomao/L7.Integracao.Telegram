using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Repository
{
    public class OrderRepository : IModelRepository<Order>
    {
        public void Add(Order entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable> ListarOrder()
        {
            throw new NotImplementedException();
        }

        public Task<Order> ListarOrderById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
