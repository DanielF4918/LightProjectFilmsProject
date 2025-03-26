using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Domain.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Services.Implementations
{
    public class RentalDetailService : IRentalDetailService
    {
        private readonly IRentalDetailsDAL _rentalDetailDAL;

        public RentalDetailService(IRentalDetailsDAL rentalDetailDAL)
        {
            _rentalDetailDAL = rentalDetailDAL;
        }

        public void Add(RentalDetail entity)
        {
            _rentalDetailDAL.Add(entity);
        }

        public void Update(RentalDetail entity)
        {
            _rentalDetailDAL.Update(entity);
        }

        public void Delete(int id)
        {
            _rentalDetailDAL.Delete(id);
        }

        public RentalDetailsDTO GetById(int id)
        {
            var rentalDetail = _rentalDetailDAL.GetById(id);
            return new RentalDetailsDTO
            {
                IdDetail = rentalDetail.IdDetail,
                IdRental = rentalDetail.IdRental,
                IdEquipment = rentalDetail.IdEquipment,
                Quantity = rentalDetail.Quantity,
                Subtotal = rentalDetail.Subtotal
            };
        }

        public IEnumerable<RentalDetailsDTO> GetAll()
        {
            return _rentalDetailDAL.GetAll().Select(rentalDetail => new RentalDetailsDTO
            {

                IdDetail = rentalDetail.IdDetail,
                IdRental = rentalDetail.IdRental,
                IdEquipment = rentalDetail.IdEquipment,
                Quantity = rentalDetail.Quantity,
                Subtotal = rentalDetail.Subtotal
            }).ToList();
        }
    }
}