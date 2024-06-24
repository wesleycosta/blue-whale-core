using Orangotango.Core.Abstractions;
using Orangotango.Core.Enums;
using Event = Orangotango.Core.Events.Event;

namespace Orangotango.Core.Bus;

public abstract class EventProcessorBase(ILoggerService _logger)
{
    protected void LogErrorProcessedWithFailure<TEvent>(TEvent @event) where TEvent : Event
    {
        var message = $"Event processed successfully {typeof(TEvent).Name}";

        _logger.Information(nameof(OperationLogs.EventProcessedSuccessfully),
            message,
            @event.TranceId);
    }
}
