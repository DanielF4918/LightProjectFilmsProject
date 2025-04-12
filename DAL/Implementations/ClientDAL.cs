using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class ClientDAL : DALGenericoImpl<Client>, IClientDAL
    {
        private readonly RentalSystem _context;

        public ClientDAL(RentalSystem context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Implementación personalizada del método Update (sobrescribe la versión base)
        /// </summary>
        public override void Update(Client entity)
        {
            _context.Set<Client>().Update(entity);
            _context.SaveChanges();
        }
    }
}

