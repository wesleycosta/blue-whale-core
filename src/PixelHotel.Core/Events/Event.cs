namespace PixelHotel.Core.Messages;

public abstract class Event 
{
    public Guid? AggregateId { get; protected set; }
    public DateTimeOffset? Timestamp { get; private set; }

    protected Event() 
        => Timestamp = DateTimeOffset.Now;
}
