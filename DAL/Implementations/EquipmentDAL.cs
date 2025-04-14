using DAL.Interfaces;
using Domain.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class EquipmentDAL : IEquipmentDAL
    {
        public Task<List<Equipment>> GetAllEquipment() => throw new NotImplementedException();
        public Task<Equipment> GetEquipmentById(int id) => throw new NotImplementedException();
        public Task<Equipment> CreateEquipment(Equipment equipment) => throw new NotImplementedException();
        public Task UpdateEquipment(Equipment equipment) => throw new NotImplementedException();
        public Task DeleteEquipment(int id) => throw new NotImplementedException();
    }
}
