using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities;

public class UnitAvailability
{
    public int Id { get; set; }

    public int UnitId { get; set; }

    public DateTime Date { get; set; }

    public bool IsAvailable { get; set; } = true;

    public decimal? SpecialPrice { get; set; }

    public int? MinNights { get; set; }

    // Navigation
    public Unit Unit { get; set; } = default!;
}