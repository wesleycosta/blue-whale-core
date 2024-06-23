using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Orangotango.Core.Services;
using System.Linq;

namespace Orangotango.Api.Filters;

public class ResultFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value is Result result)
        {
            if (result.IsValid)
            {
                objectResult.Value = result.Data;
                return;
            }

            objectResult.StatusCode = (int)result.StatusCodeError;
            objectResult.Value = new
            {
                errors = result.Validation.Errors.Select(error => error.ErrorMessage)
            };
        }
    }
}
