namespace VerticalSlice.Api.Features.Idempotency;

public static class IdempotencyExtensions
{
    public static IServiceCollection AddIdempotency(
        this IServiceCollection services)
    {
        return services
            .AddScoped(typeof(IdempotencyPipelineBehavior<,>))
            .AddScoped<IdempotentReceiver>();
    }
}
