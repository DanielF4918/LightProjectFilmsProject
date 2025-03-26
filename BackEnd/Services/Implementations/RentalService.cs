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
                TotalCost = rental.TotalCost,
                RentalDate = rental.RentalDate.ToDateTime(TimeOnly.MinValue),
                ReturnDate = rental.ReturnDate.ToDateTime(TimeOnly.MinValue)
            };
        }

        public IEnumerable<RentalDTO> GetAll()
        {
            return _rentalDAL.GetAll().Select(rental => new RentalDTO
            {
                IdRental = rental.IdRental,
                RentalDate = rental.RentalDate.ToDateTime(TimeOnly.MinValue),
                ReturnDate = rental.ReturnDate.ToDateTime(TimeOnly.MinValue),
                TotalCost = rental.TotalCost
            }).ToList();
        }
    }
}