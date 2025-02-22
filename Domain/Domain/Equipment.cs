using System;
using System.Collections.Generic;

namespace Domain.Domain;

public partial class Equipment
{
    public int IdEquipment { get; set; }

    public string EquipmentName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public decimal DailyRate { get; set; }

    public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
}
