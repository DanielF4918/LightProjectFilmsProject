using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class EventoDAL : DALGenericoImpl<Evento>, IEventoDAL
    {
        public EventoDAL(RentalSystem context) : base(context)
        {
        }
    }
}
