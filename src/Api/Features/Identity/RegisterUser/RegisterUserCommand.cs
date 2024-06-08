using MediatR;
using VerticalSlice.Api.Features.Identity.Models;

namespace VerticalSlice.Api.Features.Identity.RegisterUser;

public class RegisterUserCommand : IRequest<RegisterUserOutput>
{
    public Guid RoleId { get; set; }
    public Guid OrganizationId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public User ToUser() => new(Guid.NewGuid(), DisplayName, Email);
}
