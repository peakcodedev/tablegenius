using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database;

public static class DataServiceCollectionExtensions
{
    public static void AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<TenantProvider>();
        services.AddDbContext<RepositoryContext>();
        services.AddDbContextFactory<RepositoryContext>(
            opt => opt.UseNpgsql(connectionString,
                x => x.MigrationsAssembly("TableGenius.Api.Repo.Database")), ServiceLifetime.Scoped);
        services.Scan(scan => scan.FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IBaseRepository<>))).AsImplementedInterfaces()
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