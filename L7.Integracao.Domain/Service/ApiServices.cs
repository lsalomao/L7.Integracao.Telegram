using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L7.Integracao.Domain.Service
{
    public class ApiServices //: IApiServices
    {
        ILogger<ApiServices> _logger { get; }
        IConfiguracaoMsgRepository _repository { get; }
        ConfiguracaoMsg _configuracaoMsg;


        //public ApiServices(IConfiguracaoMsgRepository repository, ILogger<ApiServices> logger)
        //public ApiServices(ILogger<ApiServices> logger)
        public ApiServices()
        {
            //_repository = repository;
            //_logger = logger;
        }

       // public async Task<Order> PublicarOrder(Order order)
        public Order PublicarOrder(Order order)
        {
           // _logger.LogInformation("Chegamos.");

            //_configuracaoMsg = _repository.GetFirst();

            string urlServiço = "http://localhost:5000/api/telegram";
            

            try
            {
                RestClient restClient = new RestClient(new Uri(urlServiço));
                RestRequest restRequest = new RestRequest(Method.POST);
                restRequest.AddJsonBody(JsonConvert.SerializeObject(order));
                restRequest.AddHeader("content-type", "application/json");

                var retorno = restClient.Post<Order>(restRequest);

                return retorno.Data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Task<Order> IApiServices.PublicarOrder(Order order)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
