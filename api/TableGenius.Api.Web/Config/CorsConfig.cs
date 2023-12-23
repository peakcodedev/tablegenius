using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TableGenius.Api.Web.Config;

public static class CorsConfig
{
    public static IServiceCollection AddCorsConfigServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy("_myAllowSpecificOrigins",
                builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        });
        return serviceCollection;
    }

    public static IApplicationBuilder AddCorsConfig(this IApplicationBuilder app)
    {
        app.UseCors("_myAllowSpecificOrigins");
        return app;
    }
}