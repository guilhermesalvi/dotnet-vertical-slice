using Microsoft.EntityFrameworkCore;

namespace VerticalSlice.Api.Features.AuditEventProcessor;

public class AuditEventDbContext(
    DbContextOptions<AuditEventDbContext> options) : DbContext(options)
{
    public DbSet<AuditEventRecord> Records { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("audit");

        modelBuilder
            .Entity<AuditEventRecord>()
            .ToTable("Records")
            .HasKey(x => x.Id);
    }
}
