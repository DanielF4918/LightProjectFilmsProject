using BackEnd.DTO;
using Domain.Domain;
using System.Collections.Generic;

namespace BackEnd.Services.Interfaces
{
    public interface IRentalService
    {
        void Add(RentalDTO entity);
        void Update(RentalDTO entity);
        void Delete(int id);
        RentalDTO GetById(int id);
        IEnumerable<RentalDTO> GetAll();

        IEnumerable<RentalDTO> GetRentalsWithEventName();
    }
}