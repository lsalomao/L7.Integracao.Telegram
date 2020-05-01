using L7.Integracao.Domain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Repository.Interfaces
{
    public interface IConfiguracaoMsgRepository
    {
        void Add<T>(T entity);
        void Update<T>(T entity);
        void Delete<T>(T entity);

        ConfiguracaoMsg GetFirst();
        ConfiguracaoMsg ListarPorId(int id);
        IEnumerable Listar();

        Task<bool> SaveChangesAsync();
    }
}
