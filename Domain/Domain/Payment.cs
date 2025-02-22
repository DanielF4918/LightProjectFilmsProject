using System;
using System.Collections.Generic;

namespace Domain.Domain;

public partial class Payment
{
    public int IdPayment { get; set; }

    public int IdRental { get; set; }

    public decimal AmountPaid { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public DateOnly PaymentDate { get; set; }

    public virtual Rental IdRentalNavigation { get; set; } = null!;
}
