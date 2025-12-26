using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities;

public class Room
{
    public int Id { get; set; }

    public int UnitId { get; set; }

    [Required, MaxLength(100)]
    public string RoomNumber { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string RoomType { get; set; } = string.Empty; // Single, Double, Suite

    public decimal PricePerNight { get; set; }

    public int MaxOccupancy { get; set; }

    public bool IsAvailable { get; set; } = true;

    [MaxLength(500)]
    public string? Description { get; set; }

    // Navigation
    public Unit Unit { get; set; } = default!;
    public ICollection<BookingRoom> BookingRooms { get; set; } = [];
}
