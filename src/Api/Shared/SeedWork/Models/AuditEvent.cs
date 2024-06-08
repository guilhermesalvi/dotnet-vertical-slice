using MediatR;

namespace VerticalSlice.Api.Shared.SeedWork.Models;

public record AuditEvent<T>(
    Guid AggregateId,
    Guid CorrelationId,
    Guid UserId,
    string EventName,
    T Data) : INotification;
