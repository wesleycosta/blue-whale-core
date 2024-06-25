using System;

namespace Orangotango.Core.Events;

public abstract class Event
{
    public Guid AggregateId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public Guid TranceId { get; set; }

    public void SetTraceId(Guid tranceId)
        => TranceId = tranceId;
}
