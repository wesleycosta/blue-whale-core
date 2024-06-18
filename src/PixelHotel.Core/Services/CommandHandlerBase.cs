﻿using FluentValidation;
using MediatR;
using PixelHotel.Core.Abstractions;
using PixelHotel.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace PixelHotel.Core.Services;

public abstract class CommandHandlerBase<TCommand> : DataServiceBase, IRequestHandler<TCommand, Result> where TCommand : CommandBase
{
    protected IValidator<TCommand> Validator;

    protected CommandHandlerBase(IUnitOfWork unitOfWork,
        IValidator<TCommand> validator) : base(unitOfWork)
        => Validator = validator;

    public abstract Task<Result> Handle(TCommand request, CancellationToken cancellationToken);

    public async Task<bool> Validate(TCommand command)
        => await Validate(Validator, command);
}
