namespace PixelHotel.Api.Middlewares;

internal static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        => app.UseMiddleware<RequestLogMiddleware>();
}
