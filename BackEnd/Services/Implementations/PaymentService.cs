﻿using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Domain.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDAL _paymentDAL;

        public PaymentService(IPaymentDAL paymentDAL)
        {
            _paymentDAL = paymentDAL;
        }

        public void Add(Payment entity)
        {
            _paymentDAL.Add(entity);
        }

        public void Update(Payment entity)
        {
            _paymentDAL.Update(entity);
        }

        public void Delete(int id)
        {
            _paymentDAL.Delete(id);
        }

        public PaymentDTO GetById(int id)
        {
            var payment = _paymentDAL.GetById(id);
            return new PaymentDTO
            {
                IdPayment = payment.IdPayment,
<<<<<<< Updated upstream
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status
=======
                AmountPaid = payment.AmountPaid,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
>>>>>>> Stashed changes
            };
        }

        public IEnumerable<PaymentDTO> GetAll()
        {
            return _paymentDAL.GetAll().Select(payment => new PaymentDTO
            {
                IdPayment = payment.IdPayment,
<<<<<<< Updated upstream
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status
=======
                AmountPaid = payment.AmountPaid,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod
>>>>>>> Stashed changes
            }).ToList();
        }
    }
}