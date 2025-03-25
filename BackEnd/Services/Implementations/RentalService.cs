using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Domain.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Services.Implementations
{
    public class RentalService : IRentalService
    {
        private readonly IRentalDAL _rentalDAL;

        public RentalService(IRentalDAL rentalDAL)
        {
            _rentalDAL = rentalDAL;
        }

        public void Add(Rental entity)
        {
            _rentalDAL.Add(entity);
        }

        public void Update(Rental entity)
        {
            _rentalDAL.Update(entity);
        }

        public void Delete(int id)
        {
            _rentalDAL.Delete(id);
        }

        public RentalDTO GetById(int id)
        {
            var rental = _rentalDAL.GetById(id);
            return new RentalDTO
            {
                IdRental = rental.IdRental,
<<<<<<< Updated upstream
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                TotalAmount = rental.TotalAmount,
                Status = rental.Status
=======
                RentalDate = rental.RentalDate,
                ReturnDate = rental.ReturnDate,
                TotalCost = rental.TotalCost,
                IdEvent = rental.IdEvent
>>>>>>> Stashed changes
            };
        }

        public IEnumerable<RentalDTO> GetAll()
        {
            return _rentalDAL.GetAll().Select(rental => new RentalDTO
            {
                IdRental = rental.IdRental,
<<<<<<< Updated upstream
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                TotalAmount = rental.TotalAmount,
                Status = rental.Status
=======
                RentalDate = rental.RentalDate,
                ReturnDate = rental.ReturnDate,
                TotalCost = rental.TotalCost,
                IdEvent = rental.IdEvent
>>>>>>> Stashed changes
            }).ToList();
        }
    }
}