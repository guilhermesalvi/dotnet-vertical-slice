using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VerticalSlice.Api.Features.AuditEventProcessor;

public class AuditEventMap : IEntityTypeConfiguration<AuditEvent>
{
    public void Configure(EntityTypeBuilder<AuditEvent> builder)
    {
        builder
            .ToTable("Events", "audit")
            .HasKey(x => x.Id);
    }
}
