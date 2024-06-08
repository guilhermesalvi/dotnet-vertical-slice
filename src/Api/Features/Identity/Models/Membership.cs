namespace VerticalSlice.Api.Features.Identity.Models;

public record Membership(
    Guid UserId,
    Guid RoleId,
    Guid OrganizationId);
