using VerticalSlice.Api.Shared.SeedWork.Models;

namespace VerticalSlice.Api.Features.Identity.Models;

public class Organization(
    Guid id,
    string name) : Entity<Guid>(id)
{
    public string Name { get; private set; } = name;

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;
}
