using Microsoft.EntityFrameworkCore;

namespace VerticalSlice.Api.Features.Idempotency;

public class IdempotencyDbContext(
    DbContextOptions<IdempotencyDbContext> options) : DbContext(options)
{
    public DbSet<IdempotencyRecord> Records { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("idempotency");

        modelBuilder
            .Entity<IdempotencyRecord>()
            .ToTable("Records")
            .HasKey(x => x.Key);
    }
}
