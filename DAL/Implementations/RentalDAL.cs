using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class RentalDAL : DALGenericoImpl<Rental>, IRentalDAL
    {
        public RentalDAL(RentalSystem context) : base(context)
        {
        }
    }
}
