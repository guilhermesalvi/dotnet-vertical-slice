using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSlice.Api.Features.Identity.Models;

namespace VerticalSlice.Api.Features.Identity.Data;

public class PermissionMap : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder
            .ToTable("Permissions")
            .HasKey(p => p.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("Id");

        builder
            .HasIndex(x => x.Name);

        builder
            .Property(p => p.Name)
            .HasColumnName("Name")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(p => p.Description)
            .HasColumnName("Description")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();
    }
}
