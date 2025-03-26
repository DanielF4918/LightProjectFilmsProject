using BackEnd.DTO;
using Domain.Domain;
using System.Collections.Generic;

namespace BackEnd.Services.Interfaces
{
    public interface IEventoService
    {
        void Add(Evento entity);
        void Update(Evento entity);
        void Delete(int id);
        EventoDTO GetById(int id);
        IEnumerable<EventoDTO> GetAll();
    }
}