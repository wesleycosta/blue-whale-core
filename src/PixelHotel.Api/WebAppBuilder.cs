using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Infra.Configurations;
using System;
using System.Reflection;

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

    public WebAppBuilder WithServices(Assembly[] assemblies, Action<IServiceCollection, IConfiguration> configureServices)
    {
        _builder.Services.AddApiConfiguration(_builder.Configuration, assemblies);
        configureServices?.Invoke(_builder.Services, _builder.Configuration);

        return this;
    }

    public WebAppBuilder WithDefaultAppConfig()
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
