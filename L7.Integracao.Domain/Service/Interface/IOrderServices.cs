using L7.Integracao.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Service.Interface
{
    public interface IOrderServices
    {
        Task<bool> CriarOrder(Order order, Cliente cliente);
    }
}
