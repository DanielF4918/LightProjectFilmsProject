using System;
using System.Collections.Generic;

namespace Domain.Domain;

public partial class Evento
{
    public int IdEvent { get; set; }

    public string EventName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Location { get; set; } = null!;

    public int IdClient { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
