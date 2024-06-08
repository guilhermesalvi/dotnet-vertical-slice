using System.Reflection;
using FluentValidation;
using MediatR;

namespace VerticalSlice.Api.Features.RequestValidation;

public static class RequestValidationExtensions
{
    public static IServiceCollection AddRequestValidation(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationPipelineBehavior<,>));

        AssemblyScanner
            .FindValidatorsInAssemblies([Assembly.GetExecutingAssembly()])
            .ForEach(x => services.AddScoped(x.InterfaceType, x.ValidatorType));

        return services;
    }
}
