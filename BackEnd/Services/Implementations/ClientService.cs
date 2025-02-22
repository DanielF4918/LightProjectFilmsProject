using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Domain.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientDAL _clientDAL;

        public ClientService(IClientDAL clientDAL)
        {
            _clientDAL = clientDAL;
        }

        public void Add(Client entity)
        {
            _clientDAL.Add(entity);
        }

        public void Update(Client entity)
        {
            _clientDAL.Update(entity);
        }

        public void Delete(int id)
        {
            _clientDAL.Delete(id);
        }

        public ClientDTO GetById(int id)
        {
            var client = _clientDAL.GetById(id);
            return new ClientDTO
            {
                IdClient = client.IdClient,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Phone = client.Phone,
                Email = client.Email,
                Location = client.Location
            };
        }

        public IEnumerable<ClientDTO> GetAll()
        {
            return _clientDAL.GetAll().Select(client => new ClientDTO
            {
                IdClient = client.IdClient,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Phone = client.Phone,
                Email = client.Email,
                Location = client.Location
            }).ToList();
        }
    }
}
