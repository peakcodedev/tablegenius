using System.Security.Claims;
using TableGenius.Api.Web.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace TableGenius.Api.Web.Config;

public static class AuthConfig
{
    public static IServiceCollection AddAuthZero(this IServiceCollection services)
    {
        var domain = "TableGenius.eu.auth0.com";
        var audience = "https://backend.TableGenius.ch";
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = $"https://{domain}/";
            options.Audience = audience;
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.IncludeErrorDetails = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("admin",
                policy => policy.Requirements.Add(new HasScopeRequirement("admin")));
            options.AddPolicy("projectcollaborator",
                policy => policy.Requirements.Add(new HasScopeRequirement("projectcollaborator")));
            options.AddPolicy("noaccess",
                policy => policy.Requirements.Add(new HasScopeRequirement("noaccess")));
        });
        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        return services;
    }
}