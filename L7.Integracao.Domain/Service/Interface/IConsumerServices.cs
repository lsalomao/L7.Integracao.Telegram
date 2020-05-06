using System;
using System.Collections.Generic;
using System.Text;

namespace L7.Integracao.Domain.Service.Interface
{
   public interface IConsumerServices<T>
    {
        void Start(string nomeFila, Action<T> action);
    }
}
