using L7.Integracao.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace L7.Integracao.Domain.Service.Interface
{
    public interface ITelegramServices
    {
        Task Inicializar(CancellationToken cancellationToken);
        Task Stop(CancellationToken cancellationToken);
        IEnumerable<UpdateType> RequiredUpdates { get; }
    }
}
