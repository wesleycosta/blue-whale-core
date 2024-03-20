namespace PixelHotel.Api;

//public class ApiExtensions
//{
//    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
//    {
//        var tokenConvention = new RouteTokenTransformerConvention(new SlugifyParameterTransformer());
//        services.AddControllers(options => options.Conventions.Add(tokenConvention));

//        return services.AddEndpointsApiExplorer()
//            .AddAppConfiguration()
//            .AddSettings(configuration)
//            .AddInfraConfiguration(configuration)
//            .AddDomainConfiguration()
//            .AddHttpContextAccessor();
//    }

//    public static void UseApiConfiguration(this IApplicationBuilder app) =>
//        app.UseSwaggerConfiguration()
//           .UseHttpsRedirection()
//           .UseMiddlewares()
//           .UseAuthorization();
//}
