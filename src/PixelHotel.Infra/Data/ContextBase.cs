using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PixelHotel.Core.Domain;
using PixelHotel.Core.Events;
using PixelHotel.Core.Events.Abstractions;
using PixelHotel.Infra.Configurations;
using System.Reflection;

namespace PixelHotel.Infra.Data;

public abstract class ContextBase : DbContext
{
    private readonly IPublisherEvent _publisherEvent;

    protected ContextBase(IPublisherEvent publisherEvent)
    {
        _publisherEvent = publisherEvent;

        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var propertyZuadas = modelBuilder
            .Model
            .GetEntityTypes()
            .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));

        foreach (var property in propertyZuadas)
            property.SetColumnType("VARCHAR(255)");

        modelBuilder.Entity<EntityBase>().HasQueryFilter(p => p.Removed);
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        await _publisherEvent.PublishDomainEvents(this).ConfigureAwait(false);
        return await SaveChangesAsync() > 0;
    }
}
