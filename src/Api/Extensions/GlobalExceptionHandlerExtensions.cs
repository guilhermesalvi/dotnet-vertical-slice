﻿using Microsoft.AspNetCore.Diagnostics;
using VerticalSlice.Api.Shared.Notifications;
using VerticalSlice.Api.Shared.SeedWork.Models;

namespace VerticalSlice.Api.Extensions;

public static class GlobalExceptionHandlerExtensions
{
    private const string DefaultApiError = nameof(DefaultApiError);

    public static IServiceCollection AddGlobalExceptionHandler(
        this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();
        return app;
    }

    internal sealed class GlobalExceptionHandler(
        IServiceScopeFactory factory,
        ILogger<GlobalExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            using var scope = factory.CreateScope();
            var manager = scope.ServiceProvider.GetRequiredService<NotificationManager>();
            manager.AddNotification(DefaultApiError);

            var problemDetails = new ValidationProblemResult(manager.Notifications)
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error",
                Detail = "An error occurred while processing your request",
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
