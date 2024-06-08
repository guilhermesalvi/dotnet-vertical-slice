using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSlice.Api.Features.Identity.Models;

namespace VerticalSlice.Api.Features.Identity.Data;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users")
            .HasKey(u => u.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("Id");

        builder
            .HasIndex(x => x.Email);

        builder
            .Property(u => u.DisplayName)
            .HasColumnName("DisplayName")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(u => u.Email)
            .HasColumnName("Email")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();
    }
}
