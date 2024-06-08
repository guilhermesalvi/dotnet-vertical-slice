using MediatR;
using Serilog.Context;
using VerticalSlice.Api.Shared.SeedWork.Logging;
using VerticalSlice.Api.Shared.SeedWork.Models;

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
                   .WithProperty(nameof(request.IgnoreIdempotency), request.IgnoreIdempotency.ToString())))
        {
            try
            {
                switch (request.IgnoreIdempotency)
                {
                    case false
                        when await idempotentReceiver.IsProcessedAsync(request.IdempotencyKey, cancellationToken):
                        var data = await idempotentReceiver.GetDataAsync<TResponse>(request.IdempotencyKey,
                            cancellationToken);
                        return data is not null ? data : default!;
                    case false:
                        await idempotentReceiver.SetProcessedAsync(request.IdempotencyKey, cancellationToken);
                        break;
                }

                var response = await next();

                if (!request.IgnoreIdempotency)
                    await idempotentReceiver.UpdateRecordAsync(request.IdempotencyKey, response, cancellationToken);

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
