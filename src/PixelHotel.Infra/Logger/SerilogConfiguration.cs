using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace PixelHotel.Infra.Logger;

internal static class SerilogConfiguration
{
    private const string ELASTICSEARCH = "Elasticsearch";
    private const string INDEX_FORMART = "IndexFormat";
    private const string URI = "Uri";

    public static IServiceCollection AddSerilog(this IServiceCollection services,
        IConfiguration configuration)
    {
        var (indexFormat, uri) = configuration.GetElasticSearchConfigurations();
        var elasticsearchUri = new Uri(uri);
        var options = new ElasticsearchSinkOptions(elasticsearchUri)
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            IndexFormat = indexFormat
        };

        Log.Logger = new LoggerConfiguration()
            .Enrich
            .FromLogContext()
            .WriteTo
            .Elasticsearch(options)
            .WriteTo
            .Console()
            .CreateLogger();

        return services.AddSingleton(Log.Logger);
    }

    private static (string, string) GetElasticSearchConfigurations(this IConfiguration configuration)
    {
        var indexFormat = configuration
            .GetSection(ELASTICSEARCH)
            .GetSection(INDEX_FORMART)
            .Value;

        var uri = configuration
            .GetSection(ELASTICSEARCH)
            .GetSection(URI)
            .Value;

        return (indexFormat, uri);
    }
}
