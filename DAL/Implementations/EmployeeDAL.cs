using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class EmployeeDAL : DALGenericoImpl<Employee>, IEmployeeDAL
    {
        public EmployeeDAL(RentalSystem context) : base(context)
        {
        }
    }
}
