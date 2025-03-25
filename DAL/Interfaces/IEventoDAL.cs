using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Domain;

namespace DAL.Interfaces
{
    public interface IEventoDAL
    {
        Task<List<Evento>> GetAllEventos();
        Task<Evento> GetEventoById(int id);
        Task<Evento> CreateEvento(Evento evento);
        Task UpdateEvento(Evento evento);
        Task DeleteEvento(int id);
    }
}