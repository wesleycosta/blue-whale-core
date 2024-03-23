using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Core.Abstractions;
using System.Reflection;

namespace PixelHotel.Infra.Configurations;

internal static class ModuleRegisterConfigurations
{
    public static void RegisterModules(this IServiceCollection services, params Assembly[] assemblies)
    {
        var moduleType = typeof(IModuleRegister);
        var moduleTypes = assemblies.SelectMany(a => a.GetTypes())
            .Where(t => moduleType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        foreach (var type in moduleTypes)
        {
            var module = (IModuleRegister)Activator.CreateInstance(type);
            module.RegisterServices(services);
        }
    }
}
