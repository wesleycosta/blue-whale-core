using FluentValidation.Results;
using PixelHotel.Core.Database;

namespace PixelHotel.Core.Commands;

public abstract class CommandHandler
{
    private ValidationResult _validationResult;

    protected CommandHandler() =>
        _validationResult = new ValidationResult();

    public void Notify(string message) =>
        _validationResult?.Errors.Add(new ValidationFailure(string.Empty, message));

    public async Task<CommandHandlerResult> SaveData(IUnitOfWork unitOfWork, object responseCommand)
    {
        if (!await unitOfWork.Commit())
        {
            Notify("There was an error while persisting the data");
            responseCommand = null;
        }

        return new()
        {
            ValidationResult = _validationResult,
            Response = responseCommand
        };
    }

    public CommandHandlerResult Response() =>
        new()
        {
            ValidationResult = _validationResult
        };

    public CommandHandlerResult BadCommand() =>
        Response();

    public CommandHandlerResult BadCommand(string message)
    {
        Notify(message);
        return BadCommand();
    }

    public CommandHandlerResult BadCommand(Command command)
    {
        _validationResult = command.ValidationResult;
        return new()
        {
            ValidationResult = _validationResult
        };
    }

    public CommandHandlerResult BadCommand(ValidationResult validationResult)
    {
        _validationResult = validationResult;
        return new()
        {
            ValidationResult = _validationResult
        };
    }

    public CommandHandlerResult SuccessfulCommand(object responseCommand = null) =>
        new()
        {
            ValidationResult = _validationResult,
            Response = responseCommand
        };

    public static CommandHandlerResult SuccessfulCommand(CommandHandlerResult commandHandlerResult) =>
        commandHandlerResult;
}
