using System.Text.Json;
using MediatR;
using Serilog.Context;
using VerticalSlice.Api.Shared.SeedWork.Logging;

#pragma warning disable CS8603 // Possible null reference return.

namespace VerticalSlice.Api.Features.Idempotency;

public class IdempotencyPipelineBehavior<TRequest, TResponse>(
    IdempotentReceiver idempotentReceiver)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IIdempotentRequest
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        using (LogContext.Push(new LogEnricherBuilder()
                   .WithProperty(nameof(request.IdempotencyKey), request.IdempotencyKey.ToString())
                   .WithProperty(nameof(request.BypassIdempotency), request.BypassIdempotency.ToString())))
        {
            try
            {
                if (!request.BypassIdempotency &&
                    await idempotentReceiver.IsProcessedAsync(request.IdempotencyKey, cancellationToken))
                {
                    var data = await idempotentReceiver.GetResourceAsync(request.IdempotencyKey, cancellationToken);
                    return data is not null ? JsonSerializer.Deserialize<TResponse>(data) : default;
                }

                await idempotentReceiver.SetProcessedAsync(request.IdempotencyKey, cancellationToken);

                var response = await next();

                await idempotentReceiver.UpdateResourceAsync(
                    request.IdempotencyKey,
                    JsonSerializer.Serialize(response),
                    cancellationToken);

                return response;
            }
            catch
            {
                await idempotentReceiver.SetUnprocessedAsync(request.IdempotencyKey, cancellationToken);
                throw;
            }
        }
    }
}
