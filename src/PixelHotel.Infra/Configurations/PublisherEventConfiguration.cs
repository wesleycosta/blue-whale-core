using Microsoft.EntityFrameworkCore;
using PixelHotel.Core.Domain;
using PixelHotel.Core.Events.Abstractions;

namespace PixelHotel.Infra.Configurations;

internal static class PublisherEventConfiguration
{
    public static async Task PublishDomainEvents<T>(this IPublisherEvent publisher, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<EntityBase>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities
            .ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents.Select(async (domainEvent) => await publisher.Publish(domainEvent));

        await Task.WhenAll(tasks);
    }
}
