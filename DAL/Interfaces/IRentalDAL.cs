using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Interfaces
{
    public interface IRentalDAL : IDALGenerico<Rental>
    {
        IEnumerable<object> GetRentalsWithEventName();

    }
}