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
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ClientDTO> Get()
        {
            _logger.LogDebug("Obteniendo todos los clientes");
            return _clientService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<ClientDTO> Get(int id)
        {
            var client = _clientService.GetById(id);
            if (client == null)
            {
                _logger.LogWarning($"Cliente con ID {id} no encontrado.");
                return NotFound();
            }
            return client;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Client client)
        {
            if (client == null)
            {
                _logger.LogError("Intento de agregar un cliente con datos nulos.");
                return BadRequest("Client data is null.");
            }

            _clientService.Add(client);
            return CreatedAtAction(nameof(Get), new { id = client.IdClient }, client);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Client client)
        {
            try
            {
                if (client == null)
                {
                    _logger.LogError("Intento de actualizar un cliente con datos nulos.");
                    return BadRequest("Invalid client data.");
                }

                _clientService.Update(client);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al actualizar cliente: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clientService.Delete(id);
            _logger.LogInformation($"Cliente con ID {id} eliminado.");
            return NoContent();
        }
    }
}
