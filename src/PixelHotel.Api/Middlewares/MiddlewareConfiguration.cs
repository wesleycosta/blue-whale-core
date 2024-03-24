using Microsoft.AspNetCore.Builder;

namespace PixelHotel.Api.Middlewares;

internal static class MiddlewareConfiguration
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestLogMiddleware>();

        return app;
    }
}
