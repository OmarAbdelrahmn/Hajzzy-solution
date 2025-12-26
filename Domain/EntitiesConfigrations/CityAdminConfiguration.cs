using Domain.Consts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.EntitiesConfigrations;

public class CityAdminConfiguration : IEntityTypeConfiguration<CityAdmin>
{
    public void Configure(EntityTypeBuilder<CityAdmin> builder)
    {
        builder.HasKey(ca => ca.Id);

        builder.Property(ca => ca.UserId)
            .IsRequired();

        builder.HasIndex(ca => new { ca.UserId, ca.CityId })
            .IsUnique();

        builder.HasIndex(ca => ca.CityId)
            .IsUnique()
            .HasFilter("[IsActive] = 1"); // Only one active admin per city

        builder.HasOne(ca => ca.User)
            .WithMany()
            .HasForeignKey(ca => ca.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ca => ca.City)
            .WithMany(c => c.CityAdmins)
            .HasForeignKey(ca => ca.CityId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed: Assign CityAdmin to Jeddah
        builder.HasData(
            new CityAdmin
            {
                Id = 1,
                UserId = DefaultUsers.CityAdminId,
                CityId = DefaultCities.JeddahId,
                IsActive = true,
                AssignedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}