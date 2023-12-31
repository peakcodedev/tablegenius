﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database;

public static class DataServiceCollectionExtensions
{
    public static void AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<TenantProvider>();
        services.AddDbContext<RepositoryContext>(
            opt => opt.UseNpgsql(connectionString,
                x => x.MigrationsAssembly("TableGenius.Api.Repo.Database")));
        services.AddDbContextFactory<RepositoryContext>(
            opt => opt.UseNpgsql(connectionString,
                x => x.MigrationsAssembly("TableGenius.Api.Repo.Database")), ServiceLifetime.Transient);
        services.Scan(scan => scan.FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(ITenantBaseRepository<>))).AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        services.Scan(scan => scan.FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IIndependentBaseRepository<>))).AsImplementedInterfaces()
            .WithScopedLifetime()
        );
    }

    public static void UpdateDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<RepositoryContext>();
        context.Database.Migrate();
    }
}