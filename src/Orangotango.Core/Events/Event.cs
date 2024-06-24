using System;

namespace Orangotango.Core.Events;

public abstract class Event
{
    public Guid AggregateId { get; protected set; }
    public DateTimeOffset Timestamp { get; protected set; } = DateTimeOffset.UtcNow;
    public Guid TranceId { get; protected set; }

    public void SetTraceId(Guid tranceId)
        => TranceId = tranceId;
}
