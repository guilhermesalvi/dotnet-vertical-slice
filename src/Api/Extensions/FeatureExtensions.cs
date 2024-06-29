using System.Reflection;
using VerticalSlice.Api.Features.AuditEventProcessor;
using VerticalSlice.Api.Features.Idempotency;
using VerticalSlice.Api.Features.RequestValidation;

namespace VerticalSlice.Api.Extensions;

public static class FeatureExtensions
{
    public static IServiceCollection AddFeatures(
        this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddRequestValidation()
            .AddIdempotency()
            .AddAuditEventProcessor();
    }
}
