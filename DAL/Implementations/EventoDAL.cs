using DAL.Interfaces;
using Domain.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class EventoDAL : IEventoDAL
    {
        public Task<List<Evento>> GetAllEventos() => throw new NotImplementedException();
        public Task<Evento> GetEventoById(int id) => throw new NotImplementedException();
        public Task<Evento> CreateEvento(Evento evento) => throw new NotImplementedException();
        public Task UpdateEvento(Evento evento) => throw new NotImplementedException();
        public Task DeleteEvento(int id) => throw new NotImplementedException();
    }
}
