using FluentValidation.Results;

namespace PixelHotel.Core.Commands;

public sealed class CommandHandlerResult
{
    public ValidationResult ValidationResult { get; init; }
    public object Response { get; init; }

    public bool IsValid =>
        ValidationResult?.Errors?.Count == 0;
}
