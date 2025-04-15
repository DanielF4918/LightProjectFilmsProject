namespace FrontEnd.Models
{
    public class PaymentViewModel
    {
        public int IdPayment { get; set; }
        public int IdRental { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
