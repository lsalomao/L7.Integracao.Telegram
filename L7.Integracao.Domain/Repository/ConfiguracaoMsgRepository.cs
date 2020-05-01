using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace L7.Integracao.Domain.Repository
{
    public class ConfiguracaoMsgRepository : IConfiguracaoMsgRepository
    {
        public DataContext _context { get; }

        public ConfiguracaoMsgRepository(DataContext context)
        {
            _context = context;
        }


        public void Add<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable Listar()
        {
            return _context.ConfiguracoesMsg.ToList();
        }

        public ConfiguracaoMsg ListarPorId(int id)
        {
            return _context.ConfiguracoesMsg.FirstOrDefault(p => p.Id.Equals(id));
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public ConfiguracaoMsg GetFirst()
        {
            return _context.ConfiguracoesMsg.FirstOrDefault();
        }
    }
}
