using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.EntitiesConfigrations;


public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(n => n.Message)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(n => n.CreatedByRole)
            .HasMaxLength(50);

        builder.HasIndex(n => n.Type);
        builder.HasIndex(n => n.Target);
        builder.HasIndex(n => n.Priority);
        builder.HasIndex(n => n.IsSent);
        builder.HasIndex(n => n.IsScheduled);
        builder.HasIndex(n => n.ScheduledFor);
        builder.HasIndex(n => n.CreatedByUserId);
        builder.HasIndex(n => n.CreatedAt);

        builder.HasOne(n => n.CreatedBy)
            .WithMany()
            .HasForeignKey(n => n.CreatedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(n => n.TargetCity)
            .WithMany()
            .HasForeignKey(n => n.TargetCityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(n => n.TargetUnit)
            .WithMany()
            .HasForeignKey(n => n.TargetUnitId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
