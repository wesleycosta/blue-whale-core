using PixelHotel.Core.Domain.Events;

namespace PixelHotel.Events.Guests;

public class GuestCreatedOrUpdatedEvent : Event
{
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public DateOnly DateOfBirth { get; private set; }

    public GuestCreatedOrUpdatedEvent(Guid aggregateId,
        string? name,
        string? email,
        DateOnly dateOfBirth)
    {
        AggregateId = aggregateId;
        Name = name;
        Email = email;
        DateOfBirth = dateOfBirth;
    }
}
