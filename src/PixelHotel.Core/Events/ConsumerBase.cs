using MassTransit;

namespace PixelHotel.Core.Events;

public abstract class ConsumerBase<TEvent> : IConsumer<TEvent> where TEvent : class
{
    public abstract Task Consume(ConsumeContext<TEvent> context);
}
