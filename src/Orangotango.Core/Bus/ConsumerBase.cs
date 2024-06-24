using MassTransit;
using Orangotango.Core.Abstractions;
using Orangotango.Core.Enums;
using System;
using System.Threading.Tasks;
using Event = Orangotango.Core.Events.Event;

namespace Orangotango.Core.Bus;

public abstract class ConsumerBase<TEvent>(ILoggerService _logger) : IConsumer<TEvent> where TEvent : Event
{
    public abstract Task Consume(ConsumeContext<TEvent> context);

    protected void LogInfoEventReceived(TEvent @event)
    {
        var message = $"Event received ({typeof(TEvent).Name}).";
        _logger.Information(nameof(OperationLogs.EventReceived),
            message,
            @event,
            @event.TranceId);
    }

    protected void LogErrorProcessedWithFailure(TEvent @event, Exception exception)
    {
        var message = $"Event processed with failure ({typeof(TEvent).Name}).";

        _logger.Error(nameof(OperationLogs.EventProcessedWithFailure),
            message,
            exception,
            @event.TranceId);
    }
}
