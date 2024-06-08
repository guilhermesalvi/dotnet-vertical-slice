using VerticalSlice.Api.Shared.SeedWork.Models;

namespace VerticalSlice.Api.Features.Identity.Models;

public class Role(
    Guid id,
    string name,
    string description) : Entity<Guid>(id)
{
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;
}
