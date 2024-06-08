using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSlice.Api.Features.Identity.Models;

namespace VerticalSlice.Api.Features.Identity.Data;

public class RolePermissionMap : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder
            .ToTable("RolePermissions")
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder
            .HasOne<Role>()
            .WithMany()
            .HasForeignKey(rp => rp.RoleId);

        builder
            .HasOne<Permission>()
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);
    }
}
