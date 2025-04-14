using Domain.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRentalDetailDAL
    {
        Task<List<RentalDetails>> GetAllRentalDetails();
        Task<RentalDetails> GetRentalDetailsById(int id);
        Task<RentalDetails> CreateRentalDetails(RentalDetails rentalDetails);
        Task UpdateRentalDetails(RentalDetails rentalDetails);
        Task DeleteRentalDetails(int id);
    }
}
