using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Core.Abstractions;

namespace PixelHotel.Infra.Configurations;

public static class DataConfiguration
{
    public static IApplicationBuilder ApplyMigrate<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        var logger = app.ApplicationServices.GetService<ILoggerService>();
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            logger.Information("Startup", "Applying migrations");

            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<TDbContext>();

            ArgumentNullException.ThrowIfNull(context);
            var pendingMigrations = context.Database.GetPendingMigrations();
            LogMigrations(logger, pendingMigrations);

            context.Database.Migrate();
        }
        catch (Exception exception)
        {
            logger.Error("Startup", "Failed to apply migrations", exception);
            logger.CloseAndFlush();

            throw;
        }

        return app;
    }

    private static void LogMigrations(ILoggerService logger, IEnumerable<string> pendingMigrations)
    {
        if (!pendingMigrations.Any())
        {
            logger.Information("Startup", "No pending migration");
            return;
        }

        logger.Information("Startup", $"Migration applied: {string.Join(", ", pendingMigrations)}");
    }

    public static IApplicationBuilder AddStartupAndShutdownLog(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices.GetService<ILoggerService>();
        ArgumentNullException.ThrowIfNull(logger);
        logger.Information("Startup", "Application started");

        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            logger.Information("Shutdown", "Application finished");
            logger.CloseAndFlush();
        };

        return app;
    }
}
