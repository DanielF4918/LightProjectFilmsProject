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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<EmployeeDTO> Get()
        {
            _logger.LogDebug("Obteniendo todos los empleados");
            return _employeeService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeDTO> Get(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                _logger.LogWarning($"Empleado con ID {id} no encontrado.");
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                _logger.LogError("Intento de agregar un empleado con datos nulos.");
                return BadRequest("Employee data is null.");
            }

            _employeeService.Add(employee);
            return CreatedAtAction(nameof(Get), new { id = employee.IdEmployee }, employee);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    _logger.LogError("Intento de actualizar un empleado con datos nulos.");
                    return BadRequest("Invalid employee data.");
                }

                _employeeService.Update(employee);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al actualizar empleado: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);
            _logger.LogInformation($"Empleado con ID {id} eliminado.");
            return NoContent();
        }
    }
}
