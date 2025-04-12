using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Domain.Domain;

namespace BackEnd.Services.Implementations
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentDAL _equipmentDAL;

        public EquipmentService(IEquipmentDAL equipmentDAL)
        {
            _equipmentDAL = equipmentDAL;
        }

        public void Add(Equipment entity)
        {
            _equipmentDAL.Add(entity);
        }

        public void Delete(int id)
        {
            _equipmentDAL.Delete(id);
        }

        public IEnumerable<EquipmentDTO> GetAll()
        {
            return _equipmentDAL.GetAll().Select(equipment => new EquipmentDTO
            {
                IdEquipment = equipment.IdEquipment,
                EquipmentName = equipment.EquipmentName,
                Description = equipment.Description,
                Category = equipment.Category,
                DailyRate = equipment.DailyRate
            }).ToList();


        }


        public EquipmentDTO GetById(int id)
        {
            var equipment = _equipmentDAL.GetById(id);
            return new EquipmentDTO
            {
                IdEquipment = equipment.IdEquipment,
                EquipmentName = equipment.EquipmentName,
                Description = equipment.Description,
                Category = equipment.Category,
                DailyRate = equipment.DailyRate
            };
        }

        public void Update(Equipment entity)
        {
            _equipmentDAL.Update(entity);
        }
    }
}
