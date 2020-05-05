using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository;
using L7.Integracao.Domain.Repository.Interfaces;
using L7.Integracao.Domain.Service;
using L7.Integracao.Domain.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L7.Integracao.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        public readonly IModelRepository<Order> _repository;
        private readonly ISenderServices _senderServices;

        public TelegramController(IModelRepository<Order> repository, ISenderServices senderServices)
        {
            _repository = repository;
            _senderServices = senderServices;
        }

        // POST: api/Telegram
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order entity)
        {
            try
            {
                entity.DataCadastro = DateTime.Now;
                _repository.Add(entity);

                if (await _repository.SaveChangesAsync())
                {
                    _senderServices.Execute(entity);
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
                var result = await _repository.GetById(id);

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
                var result = await _repository.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
            }
        }
    }
}