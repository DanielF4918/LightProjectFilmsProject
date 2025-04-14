namespace BackEnd.DTO
{
    public class RentalDTO
    {
        public int IdRental { get; set; }
        public int IdEvent { get; set; }
        public string EventName { get; set; }
        public DateOnly RentalDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public decimal TotalCost { get; set; }
    }
}
