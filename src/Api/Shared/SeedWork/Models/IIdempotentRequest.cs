namespace VerticalSlice.Api.Shared.SeedWork.Models;

public interface IIdempotentRequest
{
    Guid IdempotencyKey { get; set; }
    bool IgnoreIdempotency { get; set; }
}
