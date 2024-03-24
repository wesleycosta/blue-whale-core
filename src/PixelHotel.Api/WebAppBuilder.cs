using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Infra.Configurations;
using System;

namespace PixelHotel.Api;

public sealed class WebAppBuilder
{
    private WebApplication _app;
    private WebApplicationBuilder _builder;

    public WebAppBuilder BuildDefault(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);

        return this;
    }

    public WebAppBuilder WithServices<TAssembly>(Action<IServiceCollection, IConfiguration> configureServices)
    {
        var assembly = typeof(TAssembly).Assembly;
        _builder.Services.AddApiConfiguration(_builder.Configuration, assembly);
        configureServices?.Invoke(_builder.Services, _builder.Configuration);

        return this;
    }

    public WebAppBuilder WithApp()
    {
        _app = _builder.Build();
        _app.UseApiConfiguration(_builder.Configuration);
        _app.MapControllers();

        return this;
    }

    public WebAppBuilder WithApplyMigrate<TDbContext>() where TDbContext : DbContext
    {
        _app.ApplyMigrate<TDbContext>();

        return this;
    }

    public WebApplication Create()
        => _app;
}
