using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities;

public class SubUnit
{
    public int Id { get; set; }

    public int UnitId { get; set; }

    [Required, MaxLength(100)]
    public string RoomNumber { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string RoomType { get; set; } = string.Empty; // Single, Double, Suite

    public SubUnitType Type { get; set; } // Room, Cottage, Tent, RVSpace

    public decimal PricePerNight { get; set; }

    public int MaxOccupancy { get; set; }

    public bool IsAvailable { get; set; } = true;

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    // Navigation
    public Unit Unit { get; set; } = default!;
    public ICollection<BookingRoom> BookingRooms { get; set; } = [];
    public ICollection<SubUniteAmenity> SubUnitAmenities { get; set; } = [];

}
