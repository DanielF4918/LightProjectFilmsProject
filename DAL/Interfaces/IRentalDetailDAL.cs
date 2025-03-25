using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Domain;

namespace DAL.Interfaces
{
    public interface IRentalDetailsDAL
    {
        Task<List<RentalDetails>> GetAllRentalDetails();
        Task<RentalDetails> GetRentalDetailsById(int id);
        Task<RentalDetails> CreateRentalDetails(RentalDetails rentalDetails);
        Task UpdateRentalDetails(RentalDetails rentalDetails);
        Task DeleteRentalDetails(int id);
    }
}