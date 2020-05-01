using L7.Integracao.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace L7.Integracao.Domain.Service
{
    public interface ISenderServices
    {
        void Execute(Order order);
    }
}
