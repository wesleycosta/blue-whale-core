using MediatR;
using PixelHotel.Core.Abstractions;
using PixelHotel.Core.Domain;

namespace PixelHotel.Core.Services;

public abstract class CommandHandlerBase<TCommand>(IUnitOfWork unitOfWork)
    : ServiceBase(unitOfWork), IRequestHandler<TCommand, Result> where TCommand : Command
{
    public abstract Task<Result> Handle(TCommand request, CancellationToken cancellationToken);
}
