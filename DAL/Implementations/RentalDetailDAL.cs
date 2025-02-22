using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class RentalDetailDAL : DALGenericoImpl<RentalDetail>, IRentalDetailDAL
    {
        public RentalDetailDAL(RentalSystem context) : base(context)
        {
        }
    }
}
