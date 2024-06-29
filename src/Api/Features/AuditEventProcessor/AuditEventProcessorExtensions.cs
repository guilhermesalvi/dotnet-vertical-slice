namespace VerticalSlice.Api.Features.AuditEventProcessor;

public static class AuditEventProcessorExtensions
{
    public static IServiceCollection AddAuditEventProcessor(
        this IServiceCollection services)
    {
        return services.AddScoped<AuditEventManager>();
    }
}
