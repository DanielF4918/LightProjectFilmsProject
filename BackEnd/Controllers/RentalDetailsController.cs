using DAL.Interfaces;
using Domain.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalDetailsController : ControllerBase
    {
        private readonly IRentalDetailDAL _rentalDetailDAL;

        public RentalDetailsController(IRentalDetailDAL rentalDetailDAL)
        {
            _rentalDetailDAL = rentalDetailDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var details = await _rentalDetailDAL.GetAllRentalDetails();
            return Ok(details);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var detail = await _rentalDetailDAL.GetRentalDetailsById(id);
            if (detail == null)
                return NotFound();

            return Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RentalDetails detail)
        {
            var created = await _rentalDetailDAL.CreateRentalDetails(detail);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RentalDetails detail)
        {
            if (id != detail.Id)
                return BadRequest();

            await _rentalDetailDAL.UpdateRentalDetails(detail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _rentalDetailDAL.DeleteRentalDetails(id);
            return NoContent();
        }
    }
}
