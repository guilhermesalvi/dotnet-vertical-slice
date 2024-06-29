using System.Text.Json;
using VerticalSlice.Api.Data.Contexts;

namespace VerticalSlice.Api.Features.AuditEventProcessor;

public class AuditEventManager(ApplicationDbContext context)
{
    public Task AppendAsync(
        Guid id,
        Guid aggregateId,
        Guid correlationId,
        string eventName,
        dynamic data,
        CancellationToken cancellationToken)
    {
        var serializedData = JsonSerializer.Serialize(data);
        var record = new AuditEvent(id, aggregateId, correlationId, eventName, serializedData);
        context.AuditEvents.Add(record);
        return context.SaveChangesAsync(cancellationToken);
    }
}
