using DAL.Implementations;
using DAL.Interfaces;
using Domain.Domain;
using Microsoft.EntityFrameworkCore;

public class RentalDAL : DALGenericoImpl<Rental>, IRentalDAL
{
    private readonly RentalSystem _context;

    public RentalDAL(RentalSystem context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<object> GetRentalsWithEventName()
    {
        return _context.Rentals
            .Include(r => r.IdEventNavigation) 
            .Select(r => new
            {
                IdRental = r.IdRental,
                IdEvent = r.IdEvent,
                EventName = r.IdEventNavigation.EventName, 
                RentalDate = r.RentalDate,
                ReturnDate = r.ReturnDate,
                TotalCost = r.TotalCost
            })
            .ToList();
    }
   
}
