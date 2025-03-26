using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using Domain.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        private readonly ILogger<EventoController> _logger;

        public EventoController(IEventoService eventoService, ILogger<EventoController> logger)
        {
            _eventoService = eventoService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<EventoDTO> Get()
        {
            _logger.LogDebug("Obteniendo todos los eventos");
            return _eventoService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<EventoDTO> Get(int id)
        {
            var evento = _eventoService.GetById(id);
            if (evento == null)
            {
                _logger.LogWarning($"Evento con ID {id} no encontrado.");
                return NotFound();
            }
            return evento;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Evento evento)
        {
            if (evento == null)
            {
                _logger.LogError("Intento de agregar un evento con datos nulos.");
                return BadRequest("Evento data is null.");
            }

            _eventoService.Add(evento);
            return CreatedAtAction(nameof(Get), new { id = evento.IdEvent }, evento);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Evento evento)
        {
            if (evento == null)
            {
                _logger.LogError("Intento de actualizar un evento con datos nulos.");
                return BadRequest("Invalid evento data.");
            }

            _eventoService.Update(evento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _eventoService.Delete(id);
            _logger.LogInformation($"Evento con ID {id} eliminado.");
            return NoContent();
        }
    }
}
