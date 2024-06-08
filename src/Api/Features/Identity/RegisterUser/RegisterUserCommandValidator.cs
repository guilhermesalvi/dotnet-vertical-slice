using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Features.Identity.Data;

namespace VerticalSlice.Api.Features.Identity.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IdentityDbContext _context;

    public RegisterUserCommandValidator(IdentityDbContext context)
    {
        _context = context;

        RuleFor(x => x.RoleId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithErrorCode("UserRoleIdEmpty")
            .MustAsync(ExistRole).WithErrorCode("UserRoleNotFound");

        RuleFor(x => x.OrganizationId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithErrorCode("UserOrganizationIdEmpty")
            .MustAsync(ExistOrganization).WithErrorCode("UserOrganizationNotFound");

        RuleFor(x => x.DisplayName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithErrorCode("UserDisplayNameEmpty")
            .Length(3, 50).WithErrorCode("UserDisplayNameInvalidLength");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithErrorCode("UserEmailEmpty")
            .Length(3, 50).WithErrorCode("UserEmailInvalidLength")
            .EmailAddress().WithErrorCode("UserEmailInvalidFormat")
            .MustAsync(BeUniqueEmail).WithErrorCode("UserEmailAlreadyExists");
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken) =>
        !await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);

    private async Task<bool> ExistOrganization(Guid organizationId, CancellationToken cancellationToken) =>
        await _context.Organizations.AnyAsync(x => x.Id == organizationId, cancellationToken);

    private async Task<bool> ExistRole(Guid roleId, CancellationToken cancellationToken) =>
        await _context.Roles.AnyAsync(x => x.Id == roleId, cancellationToken);
}
