using L7.Integracao.Domain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Repository
{
    public interface IRepository
    {
        void Add<T>(T entity);
        void Update<T>(T entity);
        void Delete<T>(T entity);

       // Obter order
        Task<IEnumerable> ListarOrder();
        Task<Order> ListarOrderById(int Id);


        Task<bool> SaveChangesAsync();
    }
}
