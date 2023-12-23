using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TableGenius.Api.Repo.Database.Interfaces;
namespace TableGenius.Api.Repo.Database;

public static class DataServiceCollectionExtensions
{
    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<RepositoryContext>();
        services.Scan(scan => scan.FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IBaseRepository<>))).AsImplementedInterfaces().WithScopedLifetime()
        );
    }
    
    public static void UpdateDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<RepositoryContext>();
        context.Database.Migrate();
    }
}