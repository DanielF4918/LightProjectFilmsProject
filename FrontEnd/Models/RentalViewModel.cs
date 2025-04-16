namespace FrontEnd.Models
{
    public class RentalViewModel
    {
        public int IdRental { get; set; }
        public int IdEvent { get; set; }
        public string EventName { get; set; }  // Para mostrar el nombre del evento en lugar del ID
        public DateOnly RentalDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public decimal TotalCost { get; set; }

    }
}
