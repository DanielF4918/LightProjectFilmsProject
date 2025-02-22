using System;
using System.Collections.Generic;

namespace Domain.Domain;

public partial class Rental
{
    public int IdRental { get; set; }

    public int IdEvent { get; set; }

    public DateOnly RentalDate { get; set; }

    public DateOnly ReturnDate { get; set; }

    public decimal TotalCost { get; set; }

    public virtual Evento IdEventNavigation { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
}
