using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities;

public class Notification
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Message { get; set; } = string.Empty;

    public NotificationType Type { get; set; }

    public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;

    // Who created this notification
    public string? CreatedByUserId { get; set; }
    public string? CreatedByRole { get; set; } // SuperAdmin, CityAdmin, etc.

    // Target audience
    public NotificationTarget Target { get; set; }
    public int? TargetCityId { get; set; }
    public int? TargetUnitId { get; set; }

    public bool IsScheduled { get; set; }
    public DateTime? ScheduledFor { get; set; }

    public bool IsSent { get; set; }
    public DateTime? SentAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ApplicationUser? CreatedBy { get; set; }
    public City? TargetCity { get; set; }
    public Unit? TargetUnit { get; set; }

    public ICollection<UserNotification> UserNotifications { get; set; } = [];
}