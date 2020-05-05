using L7.Integracao.Domain.Model;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Service.Interface
{
    public interface IApiServices
    {
        Task<Order> PublicarOrder(Order order);

        //Order PublicarOrder(Order order);
    }
}
