using MediatR;
using PixelHotel.Core.Domain;
using PixelHotel.Core.Events.Abstractions;
using PixelHotel.Core.Services;

namespace PixelHotel.Infra.Events;

public sealed class MediatorHandler(IMediator _mediator) : IMediatorHandler
{
    public async Task<Result> SendCommand<TCommand>(TCommand command) where TCommand : CommandBase
        => await _mediator.Send(command);
}
