using MassTransit;
using System.Threading.Tasks;

namespace PixelHotel.Core.Bus;

public abstract class ConsumerBase<TEvent> : IConsumer<TEvent> where TEvent : class
{
    public abstract Task Consume(ConsumeContext<TEvent> context);
}
