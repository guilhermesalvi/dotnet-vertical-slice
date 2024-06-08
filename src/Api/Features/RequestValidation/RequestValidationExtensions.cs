using MediatR;

namespace VerticalSlice.Api.Features.RequestValidation;

public static class RequestValidationExtensions
{
    public static IServiceCollection AddRequestValidation(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationPipelineBehavior<,>));
        return services;
    }
}
