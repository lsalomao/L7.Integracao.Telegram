using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Repository.Interfaces
{
    public interface IModelRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        // Obter order
        Task<IEnumerable> GetAll();
        Task<T> GetById(int Id);

        T GetByIdTelegram(string IdTelegram);

        Task<bool> SaveChangesAsync();
    }
}
