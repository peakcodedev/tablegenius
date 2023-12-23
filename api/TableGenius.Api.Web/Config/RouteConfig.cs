using Microsoft.AspNetCore.Builder;

namespace TableGenius.Api.Web.Config;

public static class RouteConfig
{
    public static IApplicationBuilder AddRouteConfig(this IApplicationBuilder app)
    {
        app.UseMvcWithDefaultRoute();

        return app;
    }
}