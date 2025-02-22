using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class PaymentDAL : DALGenericoImpl<Payment>, IPaymentDAL
    {
        public PaymentDAL(RentalSystem context) : base(context)
        {
        }
    }
}
