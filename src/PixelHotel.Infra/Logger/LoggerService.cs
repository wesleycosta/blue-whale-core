using Microsoft.AspNetCore.Http;
using PixelHotel.Core.Logger;
using Serilog;

namespace PixelHotel.Infra.Logger;

internal class LoggerService(ILogger logger,
    IHttpContextAccessor httpContextAccessor) : ILoggerService
{
    private static readonly string _templateDefault = "operation={operation}; message={message};traceId={traceId};machine={machine};version={version}";

    public Guid? GetTraceId()
    {
        var traceId = httpContextAccessor.HttpContext
            .Response
            .Headers["TraceId"];

        if (Guid.TryParse(traceId, out var id))
            return id;

        return null;
    }

    public void Information(string operation, string message, Guid? traceId = null)
        => logger.Information(_templateDefault,
            operation,
            message,
            traceId,
            MachineName,
            Version);

    public void Information(string operation, string message, object body, Guid? traceId = null)
        => logger.Information(string.Concat(_templateDefault, ";body={body}"),
            operation,
            message,
            traceId,
            MachineName,
            Version,
            body);

    public void Information(string operation, string message, object body, int statusCode, Guid? traceId = null)
        => logger.Information(string.Concat(_templateDefault, ";body={body};statusCode={statusCode}"),
            operation,
            message,
            traceId,
            MachineName,
            Version,
            body,
            statusCode);

    public void Error(string operation, string message, Exception exception, Guid? traceId = null)
        => logger.Error(string.Concat(_templateDefault, ";exception={exception}"),
            operation,
            message,
            traceId,
            MachineName,
            Version,
            exception);

    public void Error(string operation, string message, Exception exception, object request, Guid? traceId = null)
        => logger.Error(string.Concat(_templateDefault, ";exception={exception};request={request}"),
            operation,
            message,
            traceId,
            MachineName,
            Version,
            exception,
            request);

    public void CloseAndFlush()
        => Log.CloseAndFlush();

    private static string MachineName => Environment.MachineName;

    private static string Version => "1.0.0";
}
