using BackEnd.DTO;
using System.Collections.Generic;
using Domain.Domain;

namespace BackEnd.Services.Interfaces
{
    public interface IClientService
    {
        void Add(Client entity);
        void Update(Client entity);
        void Delete(int id);
        ClientDTO GetById(int id);
        IEnumerable<ClientDTO> GetAll();
    }
}
