using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities;

public class SubUniteAmenity
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public int AmenityId { get; set; }

    public bool IsAvailable { get; set; } = true;

    // Navigation
    public SubUnit Unit { get; set; } = default!;
    public Amenity Amenity { get; set; } = default!;
}
