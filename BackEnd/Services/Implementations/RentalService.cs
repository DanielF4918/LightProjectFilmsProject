using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Services.Implementations
{
    public class RentalService : IRentalService
    {
        private readonly IRentalDAL _rentalDAL;
        private readonly IEventoDAL _eventDAL; //Agregar acceso a eventos

        public RentalService(IRentalDAL rentalDAL, IEventoDAL eventDAL)
        {
            _rentalDAL = rentalDAL;
            _eventDAL = eventDAL;
        }

        public void Add(RentalDTO rentalDTO)
        {
            if (rentalDTO.IdEvent == null || rentalDTO.IdEvent == 0)
            {
                var eventEntity = _eventDAL.GetAll().FirstOrDefault(e => e.EventName == rentalDTO.EventName);
                if (eventEntity == null)
                {
                    throw new Exception($"Event '{rentalDTO.EventName}' not found.");
                }
                rentalDTO.IdEvent = eventEntity.IdEvent;
            }

            var rental = new Rental
            {
                IdEvent = rentalDTO.IdEvent,
                RentalDate = rentalDTO.RentalDate,
                ReturnDate = rentalDTO.ReturnDate,
                TotalCost = rentalDTO.TotalCost
            };

            _rentalDAL.Add(rental);
        }

        public void Update(RentalDTO rentalDTO)
        {
            var existingRental = _rentalDAL.GetById(rentalDTO.IdRental); 

            if (existingRental == null)
            {
                throw new Exception($"Rental with ID {rentalDTO.IdRental} not found.");
            }

            var rental = new Rental
            {
                IdRental = rentalDTO.IdRental,
                IdEvent = rentalDTO.IdEvent,
                RentalDate = rentalDTO.RentalDate,
                ReturnDate = rentalDTO.ReturnDate,
                TotalCost = rentalDTO.TotalCost
            };

            _rentalDAL.Update(rental); 
        }

        public void Delete(int id)
        {
            _rentalDAL.Delete(id);
        }

        public RentalDTO GetById(int id)
        {
            var rental = _rentalDAL.GetById(id);

            if (rental == null) return null;

       
            var eventName = rental.IdEventNavigation?.EventName ?? _eventDAL.GetById(rental.IdEvent)?.EventName ?? "Evento no encontrado";

            return new RentalDTO
            {
                IdRental = rental.IdRental,
                IdEvent = rental.IdEvent,
                EventName = eventName,
                RentalDate = rental.RentalDate,
                ReturnDate = rental.ReturnDate,
                TotalCost = rental.TotalCost
            };
        }



        public IEnumerable<RentalDTO> GetAll()
        {
            return _rentalDAL.GetAll().Select(rental => new RentalDTO
            {
                IdRental = rental.IdRental,
                IdEvent = rental.IdEvent,
                EventName = rental.IdEventNavigation?.EventName ?? "Unknown",
                RentalDate = rental.RentalDate,
                ReturnDate = rental.ReturnDate,
                TotalCost = rental.TotalCost
            }).ToList();
        }

        public IEnumerable<RentalDTO> GetRentalsWithEventName()
        {
            return _rentalDAL.GetRentalsWithEventName()
                .Select(r => new RentalDTO
                {
                    IdRental = (int)r.GetType().GetProperty("IdRental").GetValue(r),
                    IdEvent = (int)r.GetType().GetProperty("IdEvent").GetValue(r),
                    EventName = (string)r.GetType().GetProperty("EventName").GetValue(r),
                    RentalDate = (DateOnly)r.GetType().GetProperty("RentalDate").GetValue(r),
                    ReturnDate = (DateOnly)r.GetType().GetProperty("ReturnDate").GetValue(r),
                    TotalCost = (decimal)r.GetType().GetProperty("TotalCost").GetValue(r)
                })
                .ToList();
        }
    }
}