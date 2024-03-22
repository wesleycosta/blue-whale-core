using PixelHotel.Core.Events;
using System.Diagnostics.Tracing;

namespace PixelHotel.Core.Domain;

public abstract class EntityBase
{
    private List<Event> _events = [];

    public Guid Id { get; protected set; }
    public bool Removed { get; protected set; }
    public IReadOnlyList<Event> DomainEvents => _events;

    public void GenerateId()
        => Id = Guid.NewGuid();

    public void AddEvent(Event eventMessage)
    {
        _events ??= [];
        _events.Add(eventMessage);
    }

    public bool HasEvents()
        => DomainEvents?.Count > 0;

    public void ClearEvents() =>
        _events?.Clear();

    public void Remove()
        => Removed = true;

    public void RemoveAndAddEvent<TEvent>() where TEvent : Event, new()
    {
        var @event = new TEvent();
        @event.SetAggregateId(Id);

        Remove();
        AddEvent(@event);
    }
}
