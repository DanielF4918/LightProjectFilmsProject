using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class EquipmentDAL : DALGenericoImpl<Equipment>, IEquipmentDAL
    {
        public EquipmentDAL(RentalSystem context) : base(context)
        {
        }
    }
}
