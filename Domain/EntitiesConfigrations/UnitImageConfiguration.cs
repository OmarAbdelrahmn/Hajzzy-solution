using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.EntitiesConfigrations;


public class UnitImageConfiguration : IEntityTypeConfiguration<UnitImage>
{
    public void Configure(EntityTypeBuilder<UnitImage> builder)
    {
        builder.HasKey(ui => ui.Id);

        builder.Property(ui => ui.ImageUrl)
            .IsRequired();

        builder.Property(ui => ui.Caption)
            .HasMaxLength(200);

        builder.HasIndex(ui => new { ui.UnitId, ui.IsPrimary });
        builder.HasIndex(ui => ui.DisplayOrder);

        builder.HasOne(ui => ui.Unit)
            .WithMany(u => u.Images)
            .HasForeignKey(ui => ui.UnitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}