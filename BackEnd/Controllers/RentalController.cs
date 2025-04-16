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
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly ILogger<RentalController> _logger;

        public RentalController(IRentalService rentalService, ILogger<RentalController> logger)
        {
            _rentalService = rentalService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<RentalDTO> Get()
        {
            _logger.LogDebug("Obteniendo todas las rentas");
            return _rentalService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<RentalDTO> Get(int id)
        {
            var rental = _rentalService.GetById(id);
            if (rental == null)
            {
                _logger.LogWarning($"Renta con ID {id} no encontrada.");
                return NotFound();
            }
            return rental;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RentalDTO rental)
        {
            try
            {
                if (rental == null)
                {
                    return BadRequest("Rental data is null.");
                }

                //  Validación de IdEvent
                if (rental.IdEvent == null || rental.IdEvent == 0)
                {
                    return BadRequest("Error: IdEvent is required.");
                }

                _rentalService.Add(rental);

                return Ok(new { message = "Rental created successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating rental: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RentalDTO rentalDTO)
        {
            try
            {
                if (rentalDTO == null || id != rentalDTO.IdRental)
                {
                    _logger.LogError("Intento de actualizar una renta con datos nulos o ID no coincidente.");
                    return BadRequest("Invalid rental data or ID mismatch.");
                }

                var existingRental = _rentalService.GetById(id);
                if (existingRental == null)
                {
                    _logger.LogWarning($"Rental con ID {id} no encontrado.");
                    return NotFound($"Rental with ID {id} not found.");
                }

                //  Validación de IdEvent igual al Create
                if (rentalDTO.IdEvent == null || rentalDTO.IdEvent == 0)
                {
                    return BadRequest("Error: IdEvent is required.");
                }

                _rentalService.Update(rentalDTO);
                return Ok(new { message = "Rental updated successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la renta: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _rentalService.Delete(id);
            _logger.LogInformation($"Renta con ID {id} eliminada.");
            return NoContent();
        }

        [HttpGet("GetRentalsWithEventName")]
        public IActionResult GetRentalsWithEventName()
        {
            try
            {
                var rentals = _rentalService.GetRentalsWithEventName();
                if (rentals == null || !rentals.Any())
                {
                    return NotFound("No rentals found.");
                }
                return Ok(rentals);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving rentals: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
