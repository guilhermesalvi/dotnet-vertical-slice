using MediatR;
using VerticalSlice.Api.Features.Identity.Data;
using VerticalSlice.Api.Features.Identity.Models;

namespace VerticalSlice.Api.Features.Identity.RegisterUser;

public class RegisterUserCommandHandler(
    IdentityDbContext context) : IRequestHandler<RegisterUserCommand, RegisterUserOutput>
{
    public async Task<RegisterUserOutput> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = request.ToUser();

        context.Users.Add(user);
        context.Memberships.Add(new Membership(user.Id, request.RoleId, request.OrganizationId));
        await context.SaveChangesAsync(cancellationToken);

        return RegisterUserOutput.FromUser(user, request.RoleId, request.OrganizationId);
    }
}
