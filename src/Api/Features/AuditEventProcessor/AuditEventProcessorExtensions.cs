using Microsoft.EntityFrameworkCore;

namespace VerticalSlice.Api.Features.AuditEventProcessor;

public static class AuditEventProcessorExtensions
{
    public static IServiceCollection AddAuditEventProcessor(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext<AuditEventDbContext>(options =>
                options.UseInMemoryDatabase("AuditEventProcessor"));

        return services;
    }
}
