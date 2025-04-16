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
        public IActionResult Post([FromBody] Rental rental)
        {
            if (rental == null)
            {
                _logger.LogError("Intento de agregar una renta con datos nulos.");
                return BadRequest("Rental data is null.");
            }

            _rentalService.Add(rental);
            return CreatedAtAction(nameof(Get), new { id = rental.IdRental }, rental);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Rental rental)
        {
            if (rental == null)
            {
                _logger.LogError("Intento de actualizar una renta con datos nulos.");
                return BadRequest("Invalid rental data.");
            }

            _rentalService.Update(rental);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _rentalService.Delete(id);
            _logger.LogInformation($"Renta con ID {id} eliminada.");
            return NoContent();
        }
    }
}
