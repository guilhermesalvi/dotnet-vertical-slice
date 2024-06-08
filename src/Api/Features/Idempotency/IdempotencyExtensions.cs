using Microsoft.EntityFrameworkCore;

namespace VerticalSlice.Api.Features.Idempotency;

public static class IdempotencyExtensions
{
    public static IServiceCollection AddIdempotency(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext<IdempotencyDbContext>(options =>
                options.UseInMemoryDatabase("Idempotency"))
            .AddScoped(typeof(IdempotencyPipelineBehavior<,>))
            .AddScoped<IdempotentReceiver>();

        return services;
    }
}
