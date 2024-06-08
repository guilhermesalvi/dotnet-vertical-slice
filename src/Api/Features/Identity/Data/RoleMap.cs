using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSlice.Api.Features.Identity.Models;

namespace VerticalSlice.Api.Features.Identity.Data;

public class RoleMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .ToTable("Roles")
            .HasKey(r => r.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("Id");

        builder
            .HasIndex(x => x.Name);

        builder
            .Property(r => r.Name)
            .HasColumnName("Name")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(r => r.Description)
            .HasColumnName("Description")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();
    }
}
