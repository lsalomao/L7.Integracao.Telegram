using L7.Integracao.Domain.Data;
using L7.Integracao.Domain.Model;
using L7.Integracao.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace L7.Integracao.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        public readonly DataContext _context;
        public readonly IRepository _repository;

        public EventoController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Evento
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    try
        //    {
        //        var result = await _repository.Listar();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
        //    }
        //}

        //// GET: api/Evento/5
        //[HttpGet("{id}", Name = "Get")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    try
        //    {
        //        var result = await _repository.Listar();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
        //    }
        //}

        //// POST: api/Evento
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Evento entity)
        //{
        //    try
        //    {
        //        _repository.Add(entity);

        //        if (await _repository.SaveChangesAsync())
        //        {
        //            return Created($"/api/evento/{entity.EventoId}", entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
        //    }

        //    return BadRequest("Error");
        //}

        //// PUT: api/Evento/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] Evento entity)
        //{
        //    try
        //    {
        //        var evento = await _repository.Listar();

        //        if (evento == null) return NotFound();

        //        //evento.Nome = entity.Nome;
        //        //evento.Tema = entity.Tema;
        //        _repository.Update(evento);

        //        if (await _repository.SaveChangesAsync())
        //        {
        //            return Created($"/api/evento/{entity.EventoId}", entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
        //    }

        //    return BadRequest("Error");
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var evento = await _repository.Listar();

        //        if (evento == null) return NotFound();

        //        _repository.Delete(evento);

        //        if (await _repository.SaveChangesAsync())
        //            return Ok("Exclussão feita");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
        //    }

        //    return BadRequest("Error");
        //}
    }
}
