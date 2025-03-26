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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PaymentDTO> Get()
        {
            _logger.LogDebug("Obteniendo todos los pagos");
            return _paymentService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<PaymentDTO> Get(int id)
        {
            var payment = _paymentService.GetById(id);
            if (payment == null)
            {
                _logger.LogWarning($"Pago con ID {id} no encontrado.");
                return NotFound();
            }
            return payment;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Payment payment)
        {
            if (payment == null)
            {
                _logger.LogError("Intento de agregar un pago con datos nulos.");
                return BadRequest("Payment data is null.");
            }

            _paymentService.Add(payment);
            return CreatedAtAction(nameof(Get), new { id = payment.IdPayment }, payment);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Payment payment)
        {
            if (payment == null)
            {
                _logger.LogError("Intento de actualizar un pago con datos nulos.");
                return BadRequest("Invalid payment data.");
            }

            _paymentService.Update(payment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _paymentService.Delete(id);
            _logger.LogInformation($"Pago con ID {id} eliminado.");
            return NoContent();
        }
    }
}

