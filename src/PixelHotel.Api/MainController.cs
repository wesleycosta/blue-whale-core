using Microsoft.AspNetCore.Mvc;
using PixelHotel.Api.Filters;

namespace PixelHotel.Api;

[ApiController]
[ResultFilter]
public abstract class MainController : ControllerBase
{
}
