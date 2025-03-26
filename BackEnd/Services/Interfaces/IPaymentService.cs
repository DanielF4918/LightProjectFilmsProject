using BackEnd.DTO;
using DAL.Implementations;
using Domain.Domain;
using System.Collections.Generic;

namespace BackEnd.Services.Interfaces
{
    public interface IPaymentService
    {
        void Add(Payment entity);
        void Update(Payment entity);
        void Delete(int id);
        PaymentDTO GetById(int id);
        IEnumerable<PaymentDTO> GetAll();
    }
}