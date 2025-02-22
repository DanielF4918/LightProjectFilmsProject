using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class ClientDAL : DALGenericoImpl<Client>, IClientDAL
    {
        public ClientDAL(RentalSystem context) : base(context) { }
    }
}
