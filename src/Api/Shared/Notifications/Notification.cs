namespace VerticalSlice.Api.Shared.Notifications;

public readonly record struct Notification(string Key, string Value)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Key { get; } = Key;
    public string Value { get; } = Value;
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
}
