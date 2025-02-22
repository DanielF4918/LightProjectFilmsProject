using System;
using System.Collections.Generic;

namespace Domain.Domain;

public partial class RentalDetail
{
    public int IdDetail { get; set; }

    public int IdRental { get; set; }

    public int IdEquipment { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Equipment IdEquipmentNavigation { get; set; } = null!;

    public virtual Rental IdRentalNavigation { get; set; } = null!;
}
