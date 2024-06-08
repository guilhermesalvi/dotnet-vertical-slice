using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSlice.Api.Features.Identity.Models;

namespace VerticalSlice.Api.Features.Identity.Data;

public class MembershipMap : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder
            .ToTable("Memberships")
            .HasKey(m => new { m.UserId, m.RoleId, m.OrganizationId });

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(m => m.UserId);

        builder
            .HasOne<Role>()
            .WithMany()
            .HasForeignKey(m => m.RoleId);

        builder
            .HasOne<Organization>()
            .WithMany()
            .HasForeignKey(m => m.OrganizationId);
    }
}
