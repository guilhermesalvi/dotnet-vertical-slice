namespace VerticalSlice.Api.Features.Idempotency;

public record IdempotencyRecord(Guid Key, string? SerializedData)
{
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;
}
