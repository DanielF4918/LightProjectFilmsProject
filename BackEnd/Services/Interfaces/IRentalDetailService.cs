using BackEnd.DTO;
using DAL.Implementations;
using Domain.Domain;
using System.Collections.Generic;

namespace BackEnd.Services.Interfaces
{
    public interface IRentalDetailService
    {
        void Add(RentalDetail entity);
        void Update(RentalDetail entity);
        void Delete(int id);
        RentalDetailsDTO GetById(int id);
        IEnumerable<RentalDetailsDTO> GetAll();
    }
}