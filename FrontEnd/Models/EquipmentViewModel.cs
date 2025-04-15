namespace FrontEnd.Models
{
    public class EquipmentViewModel
    {
        public int IdEquipment { get; set; }

        public string EquipmentName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public decimal DailyRate { get; set; }
    }
}
