using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PixelHotel.Core.Abstractions;
using PixelHotel.Infra.Options;
using Serilog;

namespace PixelHotel.Infra.Logger;

internal class LoggerService(ILogger _logger,
    IHttpContextAccessor _httpContextAccessor,
    IOptions<ServiceOptions> _options) : ILoggerService
{
    private static readonly string _templateDefault = "service={service};operation={operation}; message={message};traceId={traceId};machine={machine};version={version}";

    public Guid? GetTraceId()
    {
        var traceId = _httpContextAccessor.HttpContext
            .Response
            .Headers["TraceId"];

        if (Guid.TryParse(traceId, out var id))
            return id;

        return null;
    }

    public void Information(string operation, string message, Guid? traceId = null)
        => _logger.Information(_templateDefault,
            Service,
            operation,
            message,
            traceId,
            MachineName,
            Version);

    public void Information(string operation, string message, object body, Guid? traceId = null)
        => _logger.Information(string.Concat(_templateDefault, ";body={body}"),
            Service,
            operation,
            message,
            traceId,
            MachineName,
            Version,
            body);

    public void Information(string operation, string message, object body, int statusCode, Guid? traceId = null)
        => _logger.Information(string.Concat(_templateDefault, ";body={body};statusCode={statusCode}"),
            Service,
            operation,
            message,
            traceId,
            MachineName,
            Version,
            body,
            statusCode);

    public void Error(string operation, string message, Exception exception, Guid? traceId = null)
        => _logger.Error(string.Concat(_templateDefault, ";exception={exception}"),
            Service,
            operation,
            message,
            traceId,
            MachineName,
            Version,
            exception);

    public void Error(string operation, string message, Exception exception, object request, Guid? traceId = null)
        => _logger.Error(string.Concat(_templateDefault, ";exception={exception};request={request}"),
            Service,
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

    private string Version => _options.Value.Version;
    private string Service => _options.Value.Name;
}
