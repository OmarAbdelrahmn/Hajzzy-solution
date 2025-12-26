using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.EntitiesConfigrations;

public class UnitAvailabilityConfiguration : IEntityTypeConfiguration<UnitAvailability>
{
    public void Configure(EntityTypeBuilder<UnitAvailability> builder)
    {
        builder.HasKey(ua => ua.Id);

        builder.Property(ua => ua.SpecialPrice)
            .HasPrecision(18, 2);

        builder.HasIndex(ua => new { ua.UnitId, ua.Date })
            .IsUnique();

        builder.HasOne(ua => ua.Unit)
            .WithMany(u => u.Availabilities)
            .HasForeignKey(ua => ua.UnitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
