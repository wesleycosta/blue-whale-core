using FluentValidation;
using MediatR;
using PixelHotel.Core.Abstractions;
using PixelHotel.Core.Domain;

namespace PixelHotel.Core.Services;

public abstract class CommandHandlerBase<TCommand>(IUnitOfWork unitOfWork, IValidator<TCommand> Validator)
    : ServiceBase(unitOfWork), IRequestHandler<TCommand, Result> where TCommand : CommandBase
{
    protected IValidator<TCommand> Validator;

    public abstract Task<Result> Handle(TCommand request, CancellationToken cancellationToken);

    public async Task<bool> Validate(TCommand request)
        => await Validate(Validator, request);
}
