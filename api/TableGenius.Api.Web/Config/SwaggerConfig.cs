using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace TableGenius.Api.Web.Config;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfigServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TableGenius API",
                Version = "v1",
                TermsOfService = new Uri("https://tablegenius.peakcode.dev/impressum"),
                Contact = new OpenApiContact
                {
                    Email = "tablegenius@peakcode.dev",
                    Name = "peakcode.dev",
                    Url = new Uri("https://tablegenius.peakcode.dev")
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });


        return serviceCollection;
    }

    public static IApplicationBuilder AddSwaggerConfig(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "TableGenius API"); });

        return app;
    }
}