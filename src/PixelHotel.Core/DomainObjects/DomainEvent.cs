using PixelHotel.Core.Messages;

namespace PixelHotel.Core.DomainObjects;

public abstract class DomainEvent
{
    private List<Event> _eventNotification = [];

    public IReadOnlyCollection<Event> DomainEvents
      => _eventNotification.AsReadOnly();

    public void AddEvent(Event eventMessage)
    {
        _eventNotification ??= [];
        _eventNotification.Add(eventMessage);
    }

    public bool HasEvents()
        => DomainEvents?.Count > 0;

    public void ClearEvents() =>
        _eventNotification?.Clear();
}
