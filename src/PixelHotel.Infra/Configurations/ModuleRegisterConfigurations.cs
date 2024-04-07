using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Core.Abstractions;
using PixelHotel.Core.Extensions;
using PixelHotel.Infra.Abstractions;
using System.Reflection;

namespace PixelHotel.Infra.Configurations;

public static class ModuleRegisterConfigurations
{
    public static void RegisterModules(this IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies)
    {
        var moduleRegisterTypes = services.GetTypesFromAssemblies<IModuleRegister>(assemblies);
        var moduleRegiterWithConfigurationTypes = services.GetTypesFromAssemblies<IModuleRegiterWithConfiguration>(assemblies);

        services.RegisterServicesModuleRegister(moduleRegisterTypes);
        services.RegisterServicesModuleRegiterWithConfig(configuration, moduleRegiterWithConfigurationTypes);
    }

    private static void RegisterServicesModuleRegister(this IServiceCollection services, IEnumerable<Type> moduleTypes)
    {
        foreach (var type in moduleTypes)
        {
            var module = (IModuleRegister)Activator.CreateInstance(type);
            module.RegisterServices(services);
        }
    }

    private static void RegisterServicesModuleRegiterWithConfig(this IServiceCollection services,
        IConfiguration configuration,
        IEnumerable<Type> moduleTypes)
    {
        foreach (var type in moduleTypes)
        {
            var module = (IModuleRegiterWithConfiguration)Activator.CreateInstance(type);
            module.RegisterServices(services, configuration);
        }
    }
}
