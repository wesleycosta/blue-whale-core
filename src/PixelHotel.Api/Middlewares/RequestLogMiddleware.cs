using PixelHotel.Core.Logger;

namespace PixelHotel.Api.Middlewares;

internal sealed class RequestLogMiddleware(RequestDelegate next,
    ILoggerService logger)
{
    public async Task Invoke(HttpContext context)
    {
        var traceId = Guid.NewGuid();
        var operation = $"{context.Request.Method} {context.Request.Path.Value}";

        await LogRequest(operation, traceId, context);

        try
        {
            await LogResponseAndInvokeNext(operation, traceId, context);
        }
        catch (Exception exception)
        {
            logger.Error(operation, "Request error", exception, traceId);
            throw;
        }
    }

    private async Task LogRequest(string operation, Guid traceId, HttpContext context)
    {
        var body = await ReadRequestBody(context);
        context.Response.Headers.Append("TraceId", traceId.ToString());

        logger.Information(operation, "Request received", body, traceId);
    }

    private static async Task<string> ReadRequestBody(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        context.Request.EnableBuffering();
        var streamReader = new StreamReader(context.Request.Body);
        var body = await streamReader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        return body;
    }

    private async Task LogResponseAndInvokeNext(string operation, Guid traceId, HttpContext context)
    {
        using var buffer = new MemoryStream();
        var stream = context.Response.Body;
        context.Response.Body = buffer;

        await next.Invoke(context);

        buffer.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(buffer);
        var body = await reader.ReadToEndAsync();

        buffer.Seek(0, SeekOrigin.Begin);

        await buffer.CopyToAsync(stream);
        context.Response.Body = stream;

        logger.Information(operation,
            "Response",
            body,
            context.Response.StatusCode,
            traceId);
    }
}
