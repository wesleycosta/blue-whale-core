using Microsoft.AspNetCore.Mvc;
using Orangotango.Api.Filters;

namespace Orangotango.Api;

[ApiController]
[ResultFilter]
public abstract class MainController : ControllerBase
{
}
