using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service.Interface;
using L7.Integracao.Domain.ValueObjetc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace L7.Integracao.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly IOrderServices orderServices;
        private readonly IModelRepository<Order> modelRepository;

        public TelegramController(IOrderServices orderServices, IModelRepository<Order> modelRepository)
        {
            this.orderServices = orderServices;
            this.modelRepository = modelRepository;
        }

        // POST: api/Telegram
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderVO orderVO)
        {
            try
            {
                Order entity = new Order()
                {                    
                    Descricao = orderVO.Descricao
                };

                Cliente cliente = new Cliente()
                {
                    IdTelegram = orderVO.IdTelegram
                };

                if (await orderServices.CriarOrder(entity, cliente))
                {
                    return Created($"/api/evento/{entity.Id}", entity);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
            }

            return BadRequest("Error");
        }

        // GET: api/Telegram/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await modelRepository.GetById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
            }
        }

        // GET: api/Telegram/5
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await modelRepository.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
            }
        }
    }
}