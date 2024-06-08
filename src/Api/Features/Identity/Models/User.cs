using VerticalSlice.Api.Shared.SeedWork.Models;

namespace VerticalSlice.Api.Features.Identity.Models;

public class User(
    Guid id,
    string displayName,
    string email) : Entity<Guid>(id)
{
    public string DisplayName { get; private set; } = displayName;
    public string Email { get; private set; } = email;

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;
}
