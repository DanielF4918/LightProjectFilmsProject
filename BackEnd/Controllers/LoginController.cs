using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using Domain.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IEmployeeService employeeService, ILogger<LoginController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Phone))
                return BadRequest("Datos incompletos");

            var empleados = _employeeService.GetAll();
            var empleado = empleados.FirstOrDefault(e => e.Email == login.Email && e.Phone == login.Phone);

            if (empleado == null)
            {
                _logger.LogWarning("Login fallido");
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            return Ok(new
            {
                Id = empleado.IdEmployee,
                Nombre = empleado.FirstName,
                Rol = empleado.Role
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

}

