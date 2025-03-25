namespace BackEnd.DTO
{
    public class EquipmentDTO
    {
        public int IdEquipment { get; set; }

        public string EquipmentName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public decimal DailyRate { get; set; }
    }
}
