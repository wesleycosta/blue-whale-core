using FluentValidation.Results;

namespace PixelHotel.Core.Domain.Services;

public sealed class Result
{
    public ValidationResult Validation { get; private set; }
    public object? Data { get; private set; }

    public Result(ValidationResult validation,
        object? data)
    {
        Validation = validation;
        Data = data;
    }

    public Result(ValidationResult validation)
        => Validation = validation;

    public bool IsValid
        => Validation?.Errors?.Count == 0;

    public bool HasData
        => Data is not null;
}
