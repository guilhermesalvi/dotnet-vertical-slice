using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Features.Identity.Data;

namespace VerticalSlice.Api.Features.Identity;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentity(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseInMemoryDatabase("Identity"));

        return services;
    }
}
