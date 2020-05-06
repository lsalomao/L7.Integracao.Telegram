using L7.Integracao.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace L7.Integracao.Domain.Service.Interface
{
    public interface ISenderServices
    {
        void Execute(object notification);
    }
}
