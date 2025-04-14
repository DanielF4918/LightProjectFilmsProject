using DAL.Interfaces;
using Domain.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentDAL _equipmentDAL;

        public EquipmentController(IEquipmentDAL equipmentDAL)
        {
            _equipmentDAL = equipmentDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var equipmentList = await _equipmentDAL.GetAllEquipment();
            return Ok(equipmentList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var equipment = await _equipmentDAL.GetEquipmentById(id);
            if (equipment == null)
                return NotFound();

            return Ok(equipment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Equipment equipment)
        {
            var created = await _equipmentDAL.CreateEquipment(equipment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Equipment equipment)
        {
            if (id != equipment.Id)
                return BadRequest();

            await _equipmentDAL.UpdateEquipment(equipment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _equipmentDAL.DeleteEquipment(id);
            return NoContent();
        }
    }
}
