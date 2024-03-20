using FluentValidation;
using FluentValidation.Results;
using MediatR;
using PixelHotel.Core.Messages;

namespace PixelHotel.Core.Commands;

public abstract class Command : Message, IRequest<CommandHandlerResult>
{
    public DateTimeOffset? Timestamp { get; private set; } = DateTimeOffset.Now;
    public ValidationResult ValidationResult { get; protected set; }

    public bool Validate<T>(AbstractValidator<T> abstractValidator, T instance)
    {
        ValidationResult = abstractValidator.Validate(instance);
        return ValidationResult.IsValid;
    }
}
