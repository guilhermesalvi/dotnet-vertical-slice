namespace VerticalSlice.Api.Features.Identity.Models;

public record RolePermission(
    Guid RoleId,
    Guid PermissionId);
