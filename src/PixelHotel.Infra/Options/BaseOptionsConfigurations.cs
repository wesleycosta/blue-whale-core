using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PixelHotel.Infra.Options;

internal static class BaseOptionsConfigurations
{
    public static IServiceCollection AddBaseOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceOptions>((opt) => configuration.Bind(ServiceOptions.Service, opt));
        services.Configure<ElasticsearchOptions>((opt) => configuration.Bind(ElasticsearchOptions.Elasticsearch, opt));

        return services;
    }

    public static ServiceOptions GetServiceOptions(this IConfiguration configuration)
    {
        var serviceOptions = new ServiceOptions();
        configuration.Bind(ServiceOptions.Service, serviceOptions);

        return serviceOptions;
    }
}
