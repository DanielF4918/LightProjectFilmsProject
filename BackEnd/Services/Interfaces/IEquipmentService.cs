using BackEnd.DTO;
using Domain.Domain;

namespace BackEnd.Services.Interfaces
{
    public interface IEquipmentService
    {
        void Add(Equipment entity);
        void Update(Equipment entity);
        void Delete(int id);
        EquipmentDTO GetById(int id);
        IEnumerable<EquipmentDTO> GetAll();
    }
}
