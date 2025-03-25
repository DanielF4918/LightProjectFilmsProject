using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class RentalDetailDAL : DALGenericoImpl<RentalDetail>, IRentalDetailsDAL
    {
        public RentalDetailDAL(RentalSystem context) : base(context)
        {
        }
    }
}
