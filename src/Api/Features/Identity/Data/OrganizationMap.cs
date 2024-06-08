using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSlice.Api.Features.Identity.Models;

namespace VerticalSlice.Api.Features.Identity.Data;

public class OrganizationMap : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder
            .ToTable("Organizations")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("Id");

        builder
            .Property(x => x.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(256);

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();
    }
}
