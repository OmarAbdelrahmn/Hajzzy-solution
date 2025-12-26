using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities;

public class ReviewImage
{
    public int Id { get; set; }

    public int ReviewId { get; set; }

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Review Review { get; set; } = default!;
}
