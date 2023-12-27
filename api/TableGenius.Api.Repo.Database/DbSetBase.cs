using Microsoft.EntityFrameworkCore;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database;

public class DbSetBase<T> where T : class
{
    public DbSetBase(RepositoryContext dataContext, TenantProvider tenantProvider)
    {
        DataContext = dataContext;
        TenantProvider = tenantProvider;
        DbSet = DataContext.Set<T>();
    }

    public DbSet<T> DbSet { get; }

    public RepositoryContext DataContext { get; }
    public TenantProvider TenantProvider { get; }
}