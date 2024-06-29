using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Data.Converters;
using VerticalSlice.Api.Data.Security;
using VerticalSlice.Api.Features.AuditEventProcessor;

namespace VerticalSlice.Api.Data.Contexts;

public class ApplicationDbContext(
    DataSecurityService dataSecurityService) : DbContext
{
    public DbSet<AuditEvent> AuditEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        AddProtectedPersonDataConverter(modelBuilder);
    }

    private void AddProtectedPersonDataConverter(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType.ClrType
                .GetProperties()
                .Where(p =>
                    p.PropertyType == typeof(string) &&
                    p.GetCustomAttribute<ProtectedPersonalDataAttribute>() != null);

            foreach (var property in properties)
            {
                modelBuilder
                    .Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(new ProtectedPersonalDataConverter(dataSecurityService));
            }
        }
    }
}
