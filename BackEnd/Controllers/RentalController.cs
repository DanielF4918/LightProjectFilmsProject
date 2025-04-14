using DAL.Interfaces;
using Domain.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalDAL _rentalDAL;

        public RentalController(IRentalDAL rentalDAL)
        {
            _rentalDAL = rentalDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rentals = await _rentalDAL.GetAllRentals();
            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rental = await _rentalDAL.GetRentalById(id);
            if (rental == null)
                return NotFound();

            return Ok(rental);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Rental rental)
        {
            var createdRental = await _rentalDAL.CreateRental(rental);
            return CreatedAtAction(nameof(Get), new { id = createdRental.Id }, createdRental);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rental rental)
        {
            if (id != rental.Id)
                return BadRequest();

            await _rentalDAL.UpdateRental(rental);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _rentalDAL.DeleteRental(id);
            return NoContent();
        }
    }
}
