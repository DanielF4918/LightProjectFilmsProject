namespace BackEnd.DTO
{
    public class RentalDTO
    {
        public int IdRental { get; set; }
        public int IdEvent { get; set; }
        public string EventName { get; set; }
        public DateOnly RentalDate { get; set; }
        public DateOnly ReturnDate { get; set; }
<<<<<<< Updated upstream
=======
        public int IdClient { get; set; }
>>>>>>> Stashed changes
        public decimal TotalCost { get; set; }
    }
}
