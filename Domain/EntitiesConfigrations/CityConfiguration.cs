using Domain.Consts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.EntitiesConfigrations;


public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.HasIndex(c => c.Name);
        builder.HasIndex(c => c.IsActive);

        // Seed data
        builder.HasData(
            [
                new City
                {
                    Id = DefaultCities.JeddahId,
                    Name = DefaultCities.JeddahName,
                    Country = "Saudi Arabia",
                    Description = "Major commercial hub and gateway to Mecca",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new City
                {
                    Id = DefaultCities.RiyadhId,
                    Name = DefaultCities.RiyadhName,
                    Country = "Saudi Arabia",
                    Description = "Capital city and largest metropolitan area",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new City
                {
                    Id = DefaultCities.MeccaId,
                    Name = DefaultCities.MeccaName,
                    Country = "Saudi Arabia",
                    Description = "Holiest city in Islam",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new City
                {
                    Id = DefaultCities.MedinaId,
                    Name = DefaultCities.MedinaName,
                    Country = "Saudi Arabia",
                    Description = "Second holiest city in Islam",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            ]
        );
    }
}
