﻿using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using Domain.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Filters; // ✅ Agregado para usar el filtro de roles

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
                _logger.LogWarning($"No se encontró el equipo con id {id}");
                return NotFound();
            }
            return equipment;
        }

        [HttpPost]
        [AuthorizeRole("Admin")] // ✅ Solo Admin puede crear equipos
        public IActionResult Post([FromBody] EquipmentDTO equipmentDTO)
        {
            if (equipmentDTO == null)
            {
                _logger.LogError("Intento de agregar un equipo con datos nulos.");
                return BadRequest("Equipment data is null.");
            }

            var equipment = new Equipment
            {
                EquipmentName = equipmentDTO.EquipmentName,
                Description = equipmentDTO.Description,
                Category = equipmentDTO.Category,
                DailyRate = equipmentDTO.DailyRate
            };

            _equipmentService.Add(equipment);
            return CreatedAtAction(nameof(Get), new { id = equipment.IdEquipment }, equipmentDTO);
        }

        [HttpPut("{id}")]
<<<<<<< Updated upstream
        [AuthorizeRole("Admin")] // ✅ Solo Admin puede editar equipos
        public IActionResult Put(int id, [FromBody] EquipmentDTO equipmentDTO)
=======
        public IActionResult Put(int id, [FromBody] Equipment equipment)
>>>>>>> Stashed changes
        {
            try
            {
                if (equipment == null || equipment.IdEquipment != id) // Verificamos que el ID del objeto y el de la URL coincidan
                {
                    _logger.LogError("Intento de actualizar equipo con datos incorrectos");
                    return BadRequest("Invalid equipment data");
                }

                _equipmentService.Update(equipment);
                return Ok(new { message = "Equipment editado con éxito" });
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al actualizar el equipo: {e.Message}");
                return StatusCode(500, "Error al actualizar el equipo");
            }
        }

        [HttpDelete("{id}")]
        [AuthorizeRole("Admin")] // ✅ Solo Admin puede eliminar equipos
        public IActionResult Delete(int id)
        {
            try
            {
                _equipmentService.Delete(id);
                _logger.LogInformation($"Se eliminó el equipo con el id {id}");
                return Ok(new { message = "Equipment eliminado con éxito" });
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al eliminar el equipo: {e.Message}");
                return StatusCode(500, "Error al eliminar el equipo");
            }
        }
    }
}

