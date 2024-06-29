using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Data.Contexts;
using VerticalSlice.Api.Data.Security;
using VerticalSlice.Api.Data.Settings;

namespace VerticalSlice.Api.Data.Extensions;

public static class DataExtensions
{
    public static IServiceCollection AddData(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<DataSecurityService>();

        services
            .AddOptions<DataSettings>()
            .BindConfiguration(nameof(DataSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<CacheSettings>()
            .BindConfiguration(nameof(CacheSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var dataSettings = configuration.Get<DataSettings>() ??
                           throw new InvalidOperationException("DataSettings is required");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(dataSettings.ConnectionString));

        var cacheSettings = configuration.Get<CacheSettings>() ??
                       throw new InvalidOperationException("CacheSettings is required");

        services.AddDistributedSqlServerCache(options =>
        {
            options.ConnectionString = cacheSettings.ConnectionString;
            options.SchemaName = cacheSettings.SchemaName;
            options.TableName = cacheSettings.TableName;
        });

        return services;
    }
}
