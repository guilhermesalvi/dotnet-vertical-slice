using System.Text.Json;
using MediatR;
using VerticalSlice.Api.Shared.SeedWork.Models;

namespace VerticalSlice.Api.Features.AuditEventProcessor;

public class AuditEventHandler<T>(
    AuditEventDbContext context) : INotificationHandler<AuditEvent<T>>
{
    public Task Handle(AuditEvent<T> notification, CancellationToken cancellationToken)
    {
        var record = new AuditEventRecord(
            Guid.NewGuid(),
            notification.AggregateId,
            notification.CorrelationId,
            notification.UserId,
            notification.EventName,
            JsonSerializer.Serialize(notification.Data));

        context.Records.Add(record);
        return context.SaveChangesAsync(cancellationToken);
    }
}
