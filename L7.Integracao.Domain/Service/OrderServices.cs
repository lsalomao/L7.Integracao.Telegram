using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Service
{
    public class OrderServices : IOrderServices
    {
        private readonly IModelRepository<Order> orderRepository;
        private readonly IModelRepository<Cliente> clienteRepository;
        private readonly ISenderServices senderServices;

        public OrderServices(IModelRepository<Order> orderRepository, IModelRepository<Cliente> clienteRepository, ISenderServices senderServices)
        {
            this.orderRepository = orderRepository;
            this.clienteRepository = clienteRepository;
            this.senderServices = senderServices;
        }

        public async Task<bool> CriarOrder(Order order, Cliente cliente)
        {
            var entity = clienteRepository.GetByIdTelegram(cliente.IdTelegram);

            if (entity == null)
            {
                clienteRepository.Add(cliente);
                await clienteRepository.SaveChangesAsync();
            }

            order.ClienteId = cliente.Id > 0 ? cliente.Id : entity.Id;

            orderRepository.Add(order);

            senderServices.Execute(order);

            return await orderRepository.SaveChangesAsync();
        }
    }
}
