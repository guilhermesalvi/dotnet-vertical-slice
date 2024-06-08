using System.Globalization;
using Microsoft.AspNetCore.Localization;
using VerticalSlice.Api.Shared.Notifications;

namespace VerticalSlice.Api.Extensions;

public static class NotificationExtensions
{
    private static readonly string[] SupportedCultures = ["pt-BR"];

    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        return services
            .Configure<RequestLocalizationOptions>(x =>
            {
                x.DefaultRequestCulture = new RequestCulture("pt-BR");
                x.SupportedCultures = SupportedCultures.Select(c => new CultureInfo(c)).ToList();
                x.SupportedUICultures = SupportedCultures.Select(c => new CultureInfo(c)).ToList();
            })
            .AddLocalization(x => x.ResourcesPath = "Shared/Resources")
            .AddScoped<NotificationManager>();
    }

    public static IApplicationBuilder UseNotifications(this IApplicationBuilder app)
    {
        return app.UseRequestLocalization();
    }
}
