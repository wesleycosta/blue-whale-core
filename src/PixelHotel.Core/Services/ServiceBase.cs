using FluentValidation.Results;
using PixelHotel.Core.Data;

namespace PixelHotel.Core.Commands;

public abstract class ServiceBase
{
    private ValidationResult _validationResult;

    protected ServiceBase()
        => _validationResult = new ValidationResult();

    public void Notify(string propertyName, string message)
        => _validationResult?.Errors.Add(new ValidationFailure(propertyName, message));

    public async Task<Result> SaveData(IUnitOfWork unitOfWork, object? data)
    {
        if (!await unitOfWork.Commit())
        {
            Notify(nameof(unitOfWork.Commit), "There was an error while persisting the data");
        }

        return new Result(_validationResult, data);
    }

    public Result Response()
        => new(_validationResult);

    public Result BadCommand()
        => Response();

    public Result BadCommand(string propertyName, string message)
    {
        Notify(propertyName, message);
        return BadCommand();
    }

    public Result BadCommand(Result result)
        => BadCommand(result.Validation);

    public Result BadCommand(ValidationResult validationResult)
    {
        _validationResult = validationResult;
        return new(_validationResult);
    }

    public Result SuccessfulCommand(object? data = null)
        => new(_validationResult, data);

    public static Result SuccessfulCommand(Result commandHandlerResult)
        => commandHandlerResult;
}
