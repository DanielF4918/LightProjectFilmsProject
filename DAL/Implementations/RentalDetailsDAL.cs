using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class RentalDetailsDAL : DALGenericoImpl<RentalDetails>, IRentalDetailDAL
    {
        public RentalDetailsDAL(RentalSystem context) : base(context)
        {
        }

        public Task<RentalDetails> CreateRentalDetails(RentalDetails rentalDetails)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRentalDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RentalDetails>> GetAllRentalDetails()
        {
            throw new NotImplementedException();
        }

        public Task<RentalDetails> GetRentalDetailsById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRentalDetails(RentalDetails rentalDetails)
        {
            throw new NotImplementedException();
        }
    }
}
