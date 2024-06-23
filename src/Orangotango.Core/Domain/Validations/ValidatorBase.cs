using FluentValidation;

namespace Orangotango.Core.Domain.Validations;

public abstract class ValidatorBase<TCommand> : AbstractValidator<TCommand> where TCommand : CommandBase
{
    protected const int MAX_LENGTH_STRING = 255;
}
