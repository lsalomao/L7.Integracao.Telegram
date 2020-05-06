using System;
using System.Collections.Generic;
using System.Text;

namespace L7.Integracao.Domain.Service.Interface
{
    public interface ITelegramConsumerServices
    {
        void EnviarMensagem(object mensagem, string cliente);
    }
}
