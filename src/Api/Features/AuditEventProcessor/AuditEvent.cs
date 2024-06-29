namespace VerticalSlice.Api.Features.AuditEventProcessor;

public record AuditEvent(
    Guid Id,
    Guid AggregateId,
    Guid CorrelationId,
    string EventName,
    string SerializedData);
