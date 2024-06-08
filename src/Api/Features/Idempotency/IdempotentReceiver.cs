using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace VerticalSlice.Api.Features.Idempotency;

public class IdempotentReceiver(IdempotencyDbContext context)
{
    public Task<bool> IsProcessedAsync(Guid key, CancellationToken cancellationToken)
    {
        return context.Records
            .AnyAsync(m => m.Key == key, cancellationToken);
    }

    public async Task<T?> GetDataAsync<T>(Guid key, CancellationToken cancellationToken)
    {
        var serialized = await context.Records
            .Where(m => m.Key == key)
            .Select(m => m.SerializedData)
            .FirstOrDefaultAsync(cancellationToken);

        return serialized is not null
            ? JsonSerializer.Deserialize<T>(serialized)
            : default;
    }

    public Task SetProcessedAsync(Guid key, CancellationToken cancellationToken)
    {
        context.Records.Add(new IdempotencyRecord(key, null));
        return context.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateRecordAsync<T>(Guid key, T? data, CancellationToken cancellationToken)
    {
        var serialized = data is not null ? JsonSerializer.Serialize(data) : null;
        context.Records.Update(new IdempotencyRecord(key, serialized));
        return context.SaveChangesAsync(cancellationToken);
    }

    public async Task SetUnprocessedAsync(Guid key, CancellationToken cancellationToken)
    {
        var record = await context.Records
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Key == key, cancellationToken);

        if (record is not null)
        {
            context.Records.Remove(record);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
