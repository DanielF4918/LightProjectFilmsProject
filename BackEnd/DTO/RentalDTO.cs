namespace BackEnd.DTO
{
    public class RentalDTO
    {
        public int IdRental { get; set; }
        public int IdEvent { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalCost { get; set; }
    }
}

