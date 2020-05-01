using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Repository
{
    public class EventoRepository : ModelRespotory
    {
        public EventoRepository(DataContext context) : base (context)
        {
        }

        //public async Task<IEnumerable> ListarPor(Evento entity)
        //{
        //    IQueryable<Evento> query = _context.Eventos;

        //    if (entity.EventoId > 0)
        //    {
        //        query = query.Where(p => p.EventoId.Equals(entity.EventoId));
        //    }

        //    if (!string.IsNullOrEmpty(entity.Nome))
        //    {
        //        query = query.Where(p => p.Nome.Contains(entity.Nome));
        //    }

        //    if (!string.IsNullOrEmpty(entity.Tema))
        //    {
        //        query = query.Where(p => p.Tema.Contains(entity.Tema));
        //    }

        //    return await query.ToListAsync();
        //}      
    }
}
