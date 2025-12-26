using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Domain;

public class ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : IdentityDbContext<ApplicationUser,ApplicationRole,string>(options)
{

    public required DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public required DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<City> Cities => Set<City>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<UnitType> UnitTypes => Set<UnitType>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Amenity> Amenities => Set<Amenity>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<CityAdmin> CityAdmins => Set<CityAdmin>();

    // NEW: Business entities
    public DbSet<Coupon> Coupons => Set<Coupon>();
    public DbSet<CancellationPolicy> CancellationPolicies => Set<CancellationPolicy>();
    public DbSet<PricingRule> PricingRules => Set<PricingRule>();
    public DbSet<LoyaltyProgram> LoyaltyPrograms => Set<LoyaltyProgram>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;

        //modelBuilder.Entity<Unit>().HasQueryFilter(u => !u.IsDeleted);
        //modelBuilder.Entity<Booking>().HasQueryFilter(b => !b.IsDeleted);
        //modelBuilder.Entity<ApplicationRole>().HasQueryFilter(r => !r.IsDeleted);
        //modelBuilder.Entity<City>().HasQueryFilter(c => !c.IsDeleted);
        //modelBuilder.Entity<Notification>().HasQueryFilter(n => !n.IsDeleted);
        //modelBuilder.Entity<CityAdmin>().HasQueryFilter(ca => !ca.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    // Suppress pending model changes warning
    //    optionsBuilder.ConfigureWarnings(warnings =>
    //        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

    //    // ADD: Performance optimizations
    //    optionsBuilder
    //        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // Default no-tracking
    //        .EnableSensitiveDataLogging(false) // Disable in production
    //        .EnableDetailedErrors(false); // Disable in production

    //    // ADD: Connection resilience (for cloud environments)
    //    if (Database.IsSqlServer())
    //    {
    //        optionsBuilder.UseSqlServer(options =>
    //        {
    //            options.EnableRetryOnFailure(
    //                maxRetryCount: 5,
    //                maxRetryDelay: TimeSpan.FromSeconds(30),
    //                errorNumbersToAdd: null);

    //            options.CommandTimeout(30); // 30 seconds
    //            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    //        });
    //    }
    //}

    //// ADD: SaveChanges with audit logging
    //public override int SaveChanges()
    //{
    //    AddTimestamps();
    //    AddAuditLogs();
    //    return base.SaveChanges();
    //}

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    AddTimestamps();
    //    AddAuditLogs();
    //    return await base.SaveChangesAsync(cancellationToken);
    //}

    //private void AddTimestamps()
    //{
    //    var entries = ChangeTracker.Entries()
    //        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

    //    foreach (var entry in entries)
    //    {
    //        if (entry.State == EntityState.Added)
    //        {
    //            // Set CreatedAt for new entities
    //            if (entry.Entity.GetType().GetProperty("CreatedAt") != null)
    //            {
    //                entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
    //            }
    //        }

    //        if (entry.State == EntityState.Modified)
    //        {
    //            // Set UpdatedAt for modified entities
    //            if (entry.Entity.GetType().GetProperty("UpdatedAt") != null)
    //            {
    //                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
    //            }

    //            // Prevent CreatedAt from being modified
    //            if (entry.Entity.GetType().GetProperty("CreatedAt") != null)
    //            {
    //                entry.Property("CreatedAt").IsModified = false;
    //            }
    //        }
    //    }
    //}

    //private void AddAuditLogs()
    //{
    //    // Note: In production, implement proper audit logging
    //    // This is a simplified example
    //    var entries = ChangeTracker.Entries()
    //        .Where(e => e.State == EntityState.Added ||
    //                   e.State == EntityState.Modified ||
    //                   e.State == EntityState.Deleted)
    //        .Where(e => e.Entity.GetType().GetProperty("Id") != null);

    //    foreach (var entry in entries)
    //    {
    //        // Implementation would:
    //        // 1. Serialize old/new values to JSON
    //        // 2. Get current user from HttpContext
    //        // 3. Create AuditLog entry
    //        // 4. Add to AuditLogs DbSet

    //        // Example structure:
    //        // var auditLog = new AuditLog
    //        // {
    //        //     TableName = entry.Metadata.GetTableName(),
    //        //     RecordId = entry.Property("Id").CurrentValue?.ToString(),
    //        //     Action = entry.State == EntityState.Added ? AuditAction.Create : 
    //        //              entry.State == EntityState.Modified ? AuditAction.Update : 
    //        //              AuditAction.Delete,
    //        //     OldValues = SerializeOldValues(entry),
    //        //     NewValues = SerializeNewValues(entry),
    //        //     UserId = GetCurrentUserId(),
    //        //     IpAddress = GetCurrentIpAddress()
    //        // };
    //        // AuditLogs.Add(auditLog);
    //    }
    //}
//}
}
