using Domain.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRentalDAL
    {
        Task<List<Rental>> GetAllRentals();
        Task<Rental> GetRentalById(int id);
        Task<Rental> CreateRental(Rental rental);
        Task UpdateRental(Rental rental);
        Task DeleteRental(int id);
    }
}
