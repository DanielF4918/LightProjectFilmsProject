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
        private readonly IRentalDetailDAL _rentalDetailDAL;

        public RentalDetailService(IRentalDetailDAL rentalDetailDAL)
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

        public RentalDetailDTO GetById(int id)
        {
            var rentalDetail = _rentalDetailDAL.GetById(id);
            return new RentalDetailDTO
            {
                IdRentalDetail = rentalDetail.IdRentalDetail,
                IdRental = rentalDetail.IdRental,
                IdEquipment = rentalDetail.IdEquipment,
                Quantity = rentalDetail.Quantity,
                UnitPrice = rentalDetail.UnitPrice
            };
        }

        public IEnumerable<RentalDetailDTO> GetAll()
        {
            return _rentalDetailDAL.GetAll().Select(rentalDetail => new RentalDetailDTO
            {
                IdRentalDetail = rentalDetail.IdRentalDetail,
                IdRental = rentalDetail.IdRental,
                IdEquipment = rentalDetail.IdEquipment,
                Quantity = rentalDetail.Quantity,
                UnitPrice = rentalDetail.UnitPrice
            }).ToList();
        }
    }
}