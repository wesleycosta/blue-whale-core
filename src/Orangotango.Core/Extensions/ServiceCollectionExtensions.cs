using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orangotango.Core.Domain;
using Orangotango.Core.Domain.Validations;
using Orangotango.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Orangotango.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandler<TCommand, THandler>(this IServiceCollection services)
        where TCommand : CommandBase
        where THandler : CommandHandlerBase<TCommand>
    {
        services.AddScoped<IRequestHandler<TCommand, Result>, THandler>();

        return services;
    }

    public static IServiceCollection AddValidator<TCommand, TValidator>(this IServiceCollection services)
       where TCommand : CommandBase
       where TValidator : ValidatorBase<TCommand>
    {
        services.AddTransient<IValidator<TCommand>, TValidator>();

        return services;
    }

    public static IEnumerable<Type> GetTypesFromAssemblies<TType>(this IServiceCollection _, IEnumerable<Assembly> assemblies)
    {
        var moduleType = typeof(TType);

        return assemblies.SelectMany(a => a.GetTypes())
             .Where(t => moduleType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
    }
}
