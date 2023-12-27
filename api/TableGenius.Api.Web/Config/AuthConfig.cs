using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TableGenius.Api.Settings;
using TableGenius.Api.Web.Auth;

namespace TableGenius.Api.Web.Config;

public static class AuthConfig
{
    public static IServiceCollection AddAuthZero(this IServiceCollection services, Auth0Options auth0Options)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = $"https://{auth0Options.Domain}/";
            options.Audience = auth0Options.Audience;
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
            options.AddPolicy("superadmin",
                policy => policy.Requirements.Add(new HasScopeRequirement("superadmin")));
        });
        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        return services;
    }
}