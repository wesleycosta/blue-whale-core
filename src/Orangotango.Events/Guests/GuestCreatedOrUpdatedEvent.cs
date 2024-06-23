using Orangotango.Core.Events;
using System;

namespace Orangotango.Events.Guests;

public class GuestCreatedOrUpdatedEvent : Event
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public DateOnly DateOfBirth { get; private set; }

    public GuestCreatedOrUpdatedEvent(Guid aggregateId,
        string firtName,
        string lastName,
        string email,
        DateOnly dateOfBirth)
    {
        AggregateId = aggregateId;
        FirstName = firtName;
        LastName = lastName;
        Email = email;
        DateOfBirth = dateOfBirth;
    }
}
