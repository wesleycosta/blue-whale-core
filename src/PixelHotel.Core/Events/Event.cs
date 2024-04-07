namespace PixelHotel.Core.Events;

public abstract class Event
{
    public Guid? AggregateId { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public Guid? TranceId { get; set; }

    protected Event()
        => Timestamp = DateTimeOffset.Now;

    public void SetAggregateId(Guid id)
        => AggregateId = id;

    public void SetTraceId(Guid id)
        => TranceId = id;
}
