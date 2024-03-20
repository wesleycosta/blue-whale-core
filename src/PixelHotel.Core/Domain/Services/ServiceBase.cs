using FluentValidation;
using FluentValidation.Results;
using PixelHotel.Core.Data;

namespace PixelHotel.Core.Domain.Services;

public abstract class ServiceBase
{
    private ValidationResult _validationResult;
    private readonly IUnitOfWork _unitOfWork;

    protected ServiceBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _validationResult = new ValidationResult();
    }

    protected async Task<bool> Validate<T>(IValidator<T> abstractValidator, T instance)
    {
        _validationResult = await abstractValidator.ValidateAsync(instance);
        return _validationResult.IsValid;
    }

    protected void Notify(string propertyName, string message)
        => _validationResult?.Errors.Add(new ValidationFailure(propertyName, message));

    protected async Task<Result> SaveData(object? data)
    {
        if (!await _unitOfWork.Commit())
            Notify(nameof(_unitOfWork.Commit), "There was an error while persisting the data");

        return new Result(_validationResult, data);
    }

    protected Result BadCommand()
        => new(_validationResult);

    protected Result BadCommand(string propertyName, string message)
    {
        Notify(propertyName, message);
        return BadCommand();
    }

    protected Result SuccessfulCommand(object? data = null)
        => new(_validationResult, data);
}
