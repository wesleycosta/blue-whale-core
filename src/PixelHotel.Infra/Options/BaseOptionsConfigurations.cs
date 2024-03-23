using Microsoft.Extensions.DependencyInjection;

namespace PixelHotel.Infra.Options;

internal static class BaseOptionsConfigurations
{
    public static IServiceCollection AddBaseOptions(this IServiceCollection services)
    {
        services.AddOptions<ServiceOptions>(ServiceOptions.KEY);
        services.AddOptions<ElasticsearchOptions>(ElasticsearchOptions.KEY);

        return services;
    }
}
