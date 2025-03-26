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
    public class RentalDetailController : ControllerBase
    {
        private readonly IRentalDetailService _rentalDetailService;
        private readonly ILogger<RentalDetailController> _logger;

        public RentalDetailController(IRentalDetailService rentalDetailService, ILogger<RentalDetailController> logger)
        {
            _rentalDetailService = rentalDetailService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<RentalDetailsDTO> Get()
        {
            _logger.LogDebug("Obteniendo todos los detalles de renta");
            return _rentalDetailService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<RentalDetailsDTO> Get(int id)
        {
            var rentalDetail = _rentalDetailService.GetById(id);
            if (rentalDetail == null)
            {
                _logger.LogWarning($"Detalle de renta con ID {id} no encontrado.");
                return NotFound();
            }
            return rentalDetail;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RentalDetail rentalDetail)
        {
            if (rentalDetail == null)
            {
                _logger.LogError("Intento de agregar un detalle de renta con datos nulos.");
                return BadRequest("RentalDetail data is null.");
            }

            _rentalDetailService.Add(rentalDetail);
            return CreatedAtAction(nameof(Get), new { id = rentalDetail.IdDetail }, rentalDetail);
        }

        [HttpPut]
        public IActionResult Put([FromBody] RentalDetail rentalDetail)
        {
            if (rentalDetail == null)
            {
                _logger.LogError("Intento de actualizar un detalle de renta con datos nulos.");
                return BadRequest("Invalid rentalDetail data.");
            }

            _rentalDetailService.Update(rentalDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _rentalDetailService.Delete(id);
            _logger.LogInformation($"Detalle de renta con ID {id} eliminado.");
            return NoContent();
        }
    }
}
