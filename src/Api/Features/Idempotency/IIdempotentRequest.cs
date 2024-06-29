namespace VerticalSlice.Api.Features.Idempotency;

public interface IIdempotentRequest
{
    Guid IdempotencyKey { get; set; }
    bool BypassIdempotency { get; set; }
}
