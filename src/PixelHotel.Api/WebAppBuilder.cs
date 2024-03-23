using Microsoft.AspNetCore.Builder;
using PixelHotel.Infra.Configurations;
using PixelHotel.Infra.Data;
using System.Reflection;

namespace PixelHotel.Api;

public sealed class WebAppBuilder
{
    private WebApplication? _app;

    public WebAppBuilder BuildDefault(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddApiConfiguration(builder.Configuration, Assembly.GetExecutingAssembly());

        _app = builder.Build();
        _app.UseApiConfiguration(builder.Configuration);
        _app.MapControllers();

        return this;
    }

    public WebAppBuilder WithApplyMigrate<TContext>() where TContext : ContextBase
    {
        _app.ApplyMigrate<TContext>();

        return this;
    }

    public WebApplication Create()
        => _app;
}
