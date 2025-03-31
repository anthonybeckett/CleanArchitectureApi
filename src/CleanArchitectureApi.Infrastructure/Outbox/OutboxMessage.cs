namespace CleanArchitectureApi.Infrastructure.Outbox;

public sealed class OutboxMessage(Guid id, DateTime occuredOnUtc, string type, string content)
{
    public Guid Id { get; private set; } = id;

    public DateTime OccuredOnUtc { get; private set; } = occuredOnUtc;

    public string Type { get; private set; } = type;

    public string Content { get; private set; } = content;

    public DateTime? ProcessedOnUtc { get; private set; }

    public string? Error { get; private set; }

    public void Update(DateTime processedOnUtc, string error)
    {
        ProcessedOnUtc = processedOnUtc;
        Error = error;
    }
}