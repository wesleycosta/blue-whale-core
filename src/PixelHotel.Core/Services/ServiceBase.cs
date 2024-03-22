using FluentValidation;
using FluentValidation.Results;
using PixelHotel.Core.Abstractions;

namespace PixelHotel.Core.Services;

public abstract class ServiceBase(IUnitOfWork unitOfWork)
{
    private ValidationResult _validationResult;

    protected async Task<bool> Validate<TCommand>(IValidator<TCommand> abstractValidator, TCommand request)
    {
        _validationResult = await abstractValidator.ValidateAsync(request);
        return _validationResult.IsValid;
    }

    protected void Notify(string propertyName, string message)
    {
        _validationResult ??= new();
        _validationResult.Errors.Add(new ValidationFailure(propertyName, message));
    }

    protected async Task<Result> SaveChanges(object responseData)
    {
        if (!await unitOfWork.Commit())
            Notify(nameof(unitOfWork.Commit), "There was an error while persisting");

        return new Result(_validationResult, responseData);
    }

    protected Result BadCommand()
        => new(_validationResult);

    protected Result BadCommand(string propertyName, string message)
    {
        Notify(propertyName, message);
        return BadCommand();
    }

    protected Result SuccessfulCommand(object data = null)
        => new(_validationResult, data);
}
