namespace BackEnd.DTOs
{
    public class RentalDetailDTO
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}