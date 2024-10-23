using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Assessment.Data.Entities;
using Assessment.Data.Interfaces;

namespace Assessment.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        base.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    public DbSet<Call> Calls { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public override int SaveChanges()
    {
        SaveChangesInternal();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SaveChangesInternal();
        return base.SaveChangesAsync(cancellationToken);
    }

    [Obsolete(@"
    This overload is invoked by the base.SaveChanges,
    so it's important to not include our SaveChangesInternal here because
    it will run twice if we invoke the other overloads.
    This has the added consequence of making this particular overload
    100% off-limits for developers; Do NOT call this overload!
	")]
    public override int SaveChanges(bool acceptAllChangesOnSuccess) =>
        base.SaveChanges(acceptAllChangesOnSuccess);

    [Obsolete("See note on SaveChanges(bool acceptAllChangesOnSuccess)")]
    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default) =>
        base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    
    private void SaveChangesInternal()
    {
        ApplyDeletionConfiguration(ChangeTracker);
    }
    
    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.")]
    private void ApplyDeletionConfiguration(ChangeTracker changeTracker)
    {
        foreach (var scopedEntry in changeTracker
                     .Entries()
                     .Where(x =>
                         x.State == EntityState.Deleted &&
                         (x.Metadata.BaseType != null
                             ? typeof(ISoftDelete).IsAssignableFrom(x.Metadata.BaseType.ClrType)
                             : x.Entity is ISoftDelete) &&
                         !x.Metadata.IsOwned() &&
                         x.Metadata is not EntityType { IsImplicitlyCreatedJoinEntityType: true }))
        {
            scopedEntry.Property(nameof(ISoftDelete.DateDeleted)).CurrentValue = DateTime.UtcNow;
            scopedEntry.State = EntityState.Modified;
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(builder);
    }
}