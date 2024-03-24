using FluentValidation.Results;
using System.Net;

namespace PixelHotel.Core.Services;

public sealed class Result
{
    public ValidationResult Validation { get; private set; }
    public object Data { get; private set; }
    public HttpStatusCode StatusCodeError { get; private set; } = HttpStatusCode.BadRequest;

    public Result(ValidationResult validation,
        object data)
    {
        Validation = validation;
        Data = data;
    }

    public Result(ValidationResult validation)
        => Validation = validation;

    public bool IsValid
        => Validation == null || Validation.Errors?.Count == 0;

    public void SetStatusCodeError(HttpStatusCode statusCodeError)
        => StatusCodeError = statusCodeError;
}
