using MediatR;

namespace PixelHotel.Core.Services;

public abstract interface ICommandHandlerBase<TCommand> : IRequestHandler<TCommand, Result>
{
    
}
