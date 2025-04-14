using DAL.Interfaces;
using Domain.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class RentalDAL : IRentalDAL
    {
        public Task<List<Rental>> GetAllRentals() => throw new NotImplementedException();
        public Task<Rental> GetRentalById(int id) => throw new NotImplementedException();
        public Task<Rental> CreateRental(Rental rental) => throw new NotImplementedException();
        public Task UpdateRental(Rental rental) => throw new NotImplementedException();
        public Task DeleteRental(int id) => throw new NotImplementedException();
    }
}
