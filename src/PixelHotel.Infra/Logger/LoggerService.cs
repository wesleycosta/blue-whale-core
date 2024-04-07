using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PixelHotel.Core.Abstractions;
using PixelHotel.Infra.Options;
using Serilog;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace PixelHotel.Infra.Logger;

internal class LoggerService : ILoggerService
{
    private static readonly string _templateDefault = "service={service};operation={operation}; message={message};traceId={traceId};machine={machine};version={version}";
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    private readonly ILogger _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOptions<ServiceOptions> _options;

    public LoggerService(ILogger logger,
        IHttpContextAccessor httpContextAccessor,
        IOptions<ServiceOptions> options)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _options = options;
    }

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
            SerializeToJson(body));

    public void Information(string operation, string message, object body, int statusCode, Guid? traceId = null)
        => _logger.Information(string.Concat(_templateDefault, ";body={body};statusCode={statusCode}"),
            Service,
            operation,
            message,
            traceId,
            MachineName,
            Version,
            SerializeToJson(body),
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
            SerializeToJson(request));

    public void CloseAndFlush()
        => Log.CloseAndFlush();

    private static string MachineName => Environment.MachineName;
    private string Version => _options.Value.Version;
    private string Service => _options.Value.Name;

    private static string SerializeToJson(object body)
    {
        if (body is null)
        {
            return default;
        }

        return JsonSerializer.Serialize(body, _jsonOptions);
    }
}
