namespace PixelHotel.Core.Messages;

public sealed record EventWrapper(string Type, Guid TraceId, object Event)
{
    public string Type { get; private set; } = Type;
    public Guid TraceId { get; private set; } = TraceId;
    public object Event { get; private set; } = Event;
}
