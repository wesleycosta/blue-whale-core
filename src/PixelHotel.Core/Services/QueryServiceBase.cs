using FluentValidation;
using FluentValidation.Results;
using PixelHotel.Core.Domain.Validations;
using System.Net;
using System.Threading.Tasks;

namespace PixelHotel.Core.Services;

public abstract class QueryServiceBase
{
    protected ValidationResult ValidationResult { get; private set; }

    protected async Task<bool> Validate<TCommand>(IValidator<TCommand> abstractValidator, TCommand request)
    {
        ValidationResult = await abstractValidator.ValidateAsync(request);
        return ValidationResult.IsValid;
    }

    protected void Notify(string propertyName, string message)
    {
        ValidationResult ??= new();
        ValidationResult.Errors.Add(new ValidationFailure(propertyName, message));
    }

    protected Result NotFoundResult(string field)
    {
        Notify(field, ValidatorMessages.NotFound(field));
        var resut = BadResult();
        resut.SetStatusCodeError(HttpStatusCode.NotFound);

        return resut;
    }

    protected Result BadResult()
        => new(ValidationResult);

    protected Result BadResult(string propertyName, string message)
    {
        Notify(propertyName, message);
        return BadResult();
    }

    protected Result SuccessfulResult(object data = null)
        => new(ValidationResult, data);
}
