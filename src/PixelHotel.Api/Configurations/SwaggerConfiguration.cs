using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PixelHotel.Infra.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Linq;

namespace PixelHotel.Api.Swagger;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, ServiceOptions options)
        => services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = options.Name,
                Version = "v1",
            });

            setup.SchemaFilter<EnumSchemaFilter>();
        });

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, ServiceOptions options)
        => app.UseSwagger(c => c.RouteTemplate = "api-docs/{documentName}/swagger.json")
              .UseSwaggerUI(c =>
              {
                  c.RoutePrefix = "api-docs";
                  c.SwaggerEndpoint("/api-docs/v1/swagger.json", options.Name);
                  c.DocExpansion(DocExpansion.List);
              });

    private sealed class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(n => schema.Enum.Add(new OpenApiString(n)));
            }
        }
    }
}
