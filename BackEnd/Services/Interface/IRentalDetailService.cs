using DAL.Implementations;
using Domain.Domain;
using BackEnd.DTOs;
using System.Collections.Generic;

namespace BackEnd.Services.Interfaces
{
    public interface IRentalDetailService
    {
        void Add(RentalDetails entity);
        void Update(RentalDetails entity);
        void Delete(int id);
        RentalDetailDTO GetById(int id);
        IEnumerable<RentalDetailDTO> GetAll();
    }
}