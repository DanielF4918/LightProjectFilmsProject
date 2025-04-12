namespace BackEnd.DTO
{
    public class PaymentDTO
    {
        public int IdPayment { get; set; }
        public int IdRental { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
    }
}


