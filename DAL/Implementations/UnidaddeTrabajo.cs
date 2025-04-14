using Domain.Domain;
using DAL.Interfaces;
using System;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        public IClientDAL Clients { get; }
        public IEmployeeDAL Employees { get; }
        public IEventoDAL Eventos { get; }
        public IEquipmentDAL Equipment { get; }
        public IPaymentDAL Payments { get; }
        public IRentalDAL Rentals { get; }
        public IRentalDetailDAL RentalDetails { get; }

        private readonly RentalSystem _context;

        public UnidadDeTrabajo(
            RentalSystem context,
            IClientDAL clientDAL,
            IEmployeeDAL employeeDAL,
            IEventoDAL eventoDAL,
            IEquipmentDAL equipmentDAL,
            IPaymentDAL paymentDAL,
            IRentalDAL rentalDAL,
            IRentalDetailDAL rentalDetailsDAL)
        {
            _context = context;
            Clients = clientDAL;
            Employees = employeeDAL;
            Eventos = eventoDAL;
            Equipment = equipmentDAL;
            Payments = paymentDAL;
            Rentals = rentalDAL;
            RentalDetails = rentalDetailsDAL;
        }

        public bool Complete()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
