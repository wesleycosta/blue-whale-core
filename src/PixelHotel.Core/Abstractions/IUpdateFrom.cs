using PixelHotel.Core.Domain;
using PixelHotel.Core.Events;
using System.Collections.Generic;

namespace PixelHotel.Core.Abstractions;

public interface IUpdateFrom<in TCommand, out TEvent> where TCommand : CommandBase where TEvent : Event
{
    IEnumerable<TEvent> UpdateFrom(TCommand command);
}
