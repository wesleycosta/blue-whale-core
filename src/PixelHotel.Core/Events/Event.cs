using MediatR;

namespace PixelHotel.Core.Messages;

public abstract class Event : INotification
{
    public Guid? AggregateId { get; protected set; }
    public DateTimeOffset? Timestamp { get; private set; }

    protected Event() 
        => Timestamp = DateTimeOffset.Now;
}
