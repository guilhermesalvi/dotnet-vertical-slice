using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSlice.Api.Shared.Notifications;
using VerticalSlice.Api.Shared.SeedWork.Models;

namespace VerticalSlice.Api.Features.Identity.RegisterUser;

public class RegisterUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        var router = builder
            .MapGroup("/identity/users")
            .WithTags("Identity");

        router
            .MapPost("", RegisterUser)
            .WithOpenApi()
            .WithDescription("Register a user")
            .Produces<RegisterUserOutput>()
            .Produces<ValidationProblemResult>(400)
            .Produces<ValidationProblemResult>(500);
    }

    private static async Task<IResult> RegisterUser(
        [FromServices] ISender sender,
        [FromServices] NotificationManager manager,
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return !manager.HasNotifications
            ? Results.Ok(result)
            : Results.BadRequest(new ValidationProblemResult(manager.Notifications.Select(x => x.Value)));
    }
}
