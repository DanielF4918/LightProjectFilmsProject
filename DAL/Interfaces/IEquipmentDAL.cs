using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Domain;

namespace DAL.Interfaces
{
    public interface IEquipmentDAL
    {
        Task<List<Equipment>> GetAllEquipment();
        Task<Equipment> GetEquipmentById(int id);
        Task<Equipment> CreateEquipment(Equipment equipment);
        Task UpdateEquipment(Equipment equipment);
        Task DeleteEquipment(int id);
    }
}