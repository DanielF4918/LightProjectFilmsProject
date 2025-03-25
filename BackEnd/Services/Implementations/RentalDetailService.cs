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
<<<<<<< Updated upstream
        private readonly IRentalDetailDAL _rentalDetailDAL;

        public RentalDetailService(IRentalDetailDAL rentalDetailDAL)
=======
        private readonly IRentalDetailsDAL _rentalDetailDAL;

        public RentalDetailService(IRentalDetailsDAL rentalDetailDAL)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                IdRentalDetail = rentalDetail.IdRentalDetail,
                IdRental = rentalDetail.IdRental,
                IdEquipment = rentalDetail.IdEquipment,
                Quantity = rentalDetail.Quantity,
                UnitPrice = rentalDetail.UnitPrice
=======
                IdDetail = rentalDetail.IdDetail,
                IdRental = rentalDetail.IdRental,
                IdEquipment = rentalDetail.IdEquipment,
                Quantity = rentalDetail.Quantity,
                Subtotal = rentalDetail.Subtotal
>>>>>>> Stashed changes
            };
        }

        public IEnumerable<RentalDetailDTO> GetAll()
        {
            return _rentalDetailDAL.GetAll().Select(rentalDetail => new RentalDetailDTO
            {
<<<<<<< Updated upstream
                IdRentalDetail = rentalDetail.IdRentalDetail,
                IdRental = rentalDetail.IdRental,
                IdEquipment = rentalDetail.IdEquipment,
                Quantity = rentalDetail.Quantity,
                UnitPrice = rentalDetail.UnitPrice
=======
                IdDetail = rentalDetail.IdDetail,
                IdRental = rentalDetail.IdRental,
                IdEquipment = rentalDetail.IdEquipment,
                Quantity = rentalDetail.Quantity,
                Subtotal = rentalDetail.Subtotal
>>>>>>> Stashed changes
            }).ToList();
        }
    }
}