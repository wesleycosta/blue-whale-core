﻿using Microsoft.EntityFrameworkCore;
using PixelHotel.Core.Domain;
using PixelHotel.Core.Events;

namespace PixelHotel.Infra.Extensions;

internal static class PublisherEventExtensions
{
    public static async Task PublishDomainEvents<T>(this IPublisherEvent publisher, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities
            .ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents.Select(async (domainEvent) => await publisher.PublishEvent(domainEvent));

        await Task.WhenAll(tasks);
    }
}
