using VerticalSlice.Api.Features.Identity.Models;

namespace VerticalSlice.Api.Features.Identity.RegisterUser;

public class RegisterUserOutput
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid OrganizationId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public static RegisterUserOutput FromUser(User user, Guid roleId, Guid organizationId) => new()
    {
        Id = user.Id,
        RoleId = roleId,
        OrganizationId = organizationId,
        DisplayName = user.DisplayName,
        Email = user.Email
    };
}
