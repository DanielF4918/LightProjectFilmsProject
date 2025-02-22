using Domain.Domain;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        public IClientDAL Clients { get; set; }
        public IEmployeeDAL Employees { get; set; }
        public IEventoDAL Eventos { get; set; }  // 🔥 Asegúrate de que esté aquí
        public IEquipmentDAL Equipment { get; set; }
        public IPaymentDAL Payments { get; set; }
        public IRentalDAL Rentals { get; set; }
        public IRentalDetailDAL RentalDetails { get; set; }

        private readonly RentalSystem _context;

        public UnidadDeTrabajo(RentalSystem context,
                               IClientDAL clientDAL,
                               IEmployeeDAL employeeDAL,
                               IEventoDAL eventoDAL,  
                               IEquipmentDAL equipmentDAL,
                               IPaymentDAL paymentDAL,
                               IRentalDAL rentalDAL,
                               IRentalDetailDAL rentalDetailDAL)
        {
            _context = context;
            Clients = clientDAL;
            Employees = employeeDAL;
            Eventos = eventoDAL; 
            Equipment = equipmentDAL;
            Payments = paymentDAL;
            Rentals = rentalDAL;
            RentalDetails = rentalDetailDAL;
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
