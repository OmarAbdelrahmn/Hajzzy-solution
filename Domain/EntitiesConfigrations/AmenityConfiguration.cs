using Domain.Consts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.EntitiesConfigrations;


public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
{
    public void Configure(EntityTypeBuilder<Amenity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Description)
            .HasMaxLength(200);

        builder.Property(a => a.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(a => a.Name);
        builder.HasIndex(a => a.Category);

        // Seed data
        //builder.HasData(
        //    [
        //        // Basic
        //        new Amenity { Id = DefaultAmenities.WiFiId, Name = "WiFi", Category = "Basic", Description = "Free high-speed wireless internet" },
        //        new Amenity { Id = DefaultAmenities.AirConditioningId, Name = "Air Conditioning", Category = "Basic", Description = "Climate control system" },
        //        new Amenity { Id = DefaultAmenities.HeatingId, Name = "Heating", Category = "Basic", Description = "Heating system" },
        //        new Amenity { Id = DefaultAmenities.KitchenId, Name = "Kitchen", Category = "Basic", Description = "Fully equipped kitchen" },
        //        new Amenity { Id = DefaultAmenities.ParkingId, Name = "Parking", Category = "Basic", Description = "Free parking space" },
                
        //        // Entertainment
        //        new Amenity { Id = DefaultAmenities.TVId, Name = "TV", Category = "Entertainment", Description = "Flat-screen television" },
        //        new Amenity { Id = DefaultAmenities.PoolId, Name = "Pool", Category = "Entertainment", Description = "Swimming pool" },
        //        new Amenity { Id = DefaultAmenities.GymId, Name = "Gym", Category = "Entertainment", Description = "Fitness center" },
        //        new Amenity { Id = DefaultAmenities.SpaId, Name = "Spa", Category = "Entertainment", Description = "Spa and wellness center" },
                
        //        // Safety
        //        new Amenity { Id = DefaultAmenities.FireExtinguisherId, Name = "Fire Extinguisher", Category = "Safety", Description = "Fire safety equipment" },
        //        new Amenity { Id = DefaultAmenities.FirstAidKitId, Name = "First Aid Kit", Category = "Safety", Description = "Medical emergency kit" },
        //        new Amenity { Id = DefaultAmenities.SecurityCamerasId, Name = "Security Cameras", Category = "Safety", Description = "24/7 surveillance" },
        //        new Amenity { Id = DefaultAmenities.SmokeDetectorId, Name = "Smoke Detector", Category = "Safety", Description = "Smoke detection system" },
                
        //        // Services
        //        new Amenity { Id = DefaultAmenities.BreakfastId, Name = "Breakfast", Category = "Services", Description = "Complimentary breakfast" },
        //        new Amenity { Id = DefaultAmenities.RoomServiceId, Name = "Room Service", Category = "Services", Description = "24-hour room service" },
        //        new Amenity { Id = DefaultAmenities.LaundryId, Name = "Laundry", Category = "Services", Description = "Laundry and dry cleaning" },
        //        new Amenity { Id = DefaultAmenities.ConciergeId, Name = "Concierge", Category = "Services", Description = "Concierge service" },
        //        new Amenity { Id = DefaultAmenities.AirportShuttleId, Name = "Airport Shuttle", Category = "Services", Description = "Free airport transportation" }
        //    ]
        //);
    }
}