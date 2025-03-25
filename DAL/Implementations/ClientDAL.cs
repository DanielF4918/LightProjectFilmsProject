using Domain.Domain;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Implementations
{
    public class ClientDAL : DALGenericoImpl<Client>, IClientDAL
    {
        private readonly RentalSystem _context;

        public ClientDAL(RentalSystem context) : base(context)
        {
            _context = context;
        }

        public void Update(Client entity)
        {
            _context.Set<Client>().Update(entity);
            _context.SaveChanges();
        }

    }
}