using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using Domain.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(IEquipmentService equipmentService, ILogger<EquipmentController> logger)
        {
            _equipmentService = equipmentService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<EquipmentDTO> Get()
        {
            _logger.LogDebug("Obteniendo todos los equipos");
            return _equipmentService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<EquipmentDTO> Get(int id)
        {
            var equipment = _equipmentService.GetById(id);
            if (equipment == null)
            {
                _logger.LogWarning($"Equipo con ID {id} no encontrado.");
                return NotFound();
            }
            return equipment;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Equipment equipment)
        {
            if (equipment == null)
            {
                _logger.LogError("Intento de agregar un equipo con datos nulos.");
                return BadRequest("Equipment data is null.");
            }

            _equipmentService.Add(equipment);
            return CreatedAtAction(nameof(Get), new { id = equipment.IdEquipment }, equipment);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Equipment equipment)
        {
            if (equipment == null)
            {
                _logger.LogError("Intento de actualizar un equipo con datos nulos.");
                return BadRequest("Invalid equipment data.");
            }

            _equipmentService.Update(equipment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _equipmentService.Delete(id);
            _logger.LogInformation($"Equipo con ID {id} eliminado.");
            return NoContent();
        }
    }
}

