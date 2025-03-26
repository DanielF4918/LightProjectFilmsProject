using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Domain.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Services.Implementations
{
    public class EventoService : IEventoService
    {
        private readonly IEventoDAL _eventoDAL;

        public EventoService(IEventoDAL eventoDAL)
        {
            _eventoDAL = eventoDAL;
        }

        public void Add(Evento entity)
        {
            _eventoDAL.Add(entity);
        }

        public void Update(Evento entity)
        {
            _eventoDAL.Update(entity);
        }

        public void Delete(int id)
        {
            _eventoDAL.Delete(id);
        }

        public EventoDTO GetById(int id)
        {
            var evento = _eventoDAL.GetById(id);
            return new EventoDTO
            {
<<<<<<< Updated upstream
                IdEvento = evento.IdEvento,
                Name = evento.Name,
                Description = evento.Description,
                StartDate = evento.StartDate,
                EndDate = evento.EndDate,
                Location = evento.Location,
                Status = evento.Status
=======
                IdEvent = evento.IdEvent,
                EventName = evento.EventName,
                StartDate = evento.StartDate,
                EndDate = evento.EndDate,
                Location = evento.Location
>>>>>>> Stashed changes
            };
        }

        public IEnumerable<EventoDTO> GetAll()
        {
            return _eventoDAL.GetAll().Select(evento => new EventoDTO
            {
<<<<<<< Updated upstream
                IdEvento = evento.IdEvento,
                Name = evento.Name,
                Description = evento.Description,
                StartDate = evento.StartDate,
                EndDate = evento.EndDate,
                Location = evento.Location,
                Status = evento.Status
=======
                IdEvent = evento.IdEvent,
                EventName = evento.EventName,
                StartDate = evento.StartDate,
                EndDate = evento.EndDate,
                Location = evento.Location,
                IdClient = evento.IdClient
>>>>>>> Stashed changes
            }).ToList();
        }
    }
}