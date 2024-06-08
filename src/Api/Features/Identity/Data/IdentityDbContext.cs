using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Features.Identity.Models;

namespace VerticalSlice.Api.Features.Identity.Data;

public class IdentityDbContext(
    DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("identity");

        modelBuilder.ApplyConfiguration(new MembershipMap());
        modelBuilder.ApplyConfiguration(new MembershipMap());
        modelBuilder.ApplyConfiguration(new PermissionMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new RolePermissionMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}
