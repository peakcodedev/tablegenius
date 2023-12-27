using System;
using System.Linq;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database.Repositories;

public class TableRepository(RepositoryContext dataContext, TenantProvider tenantProvider)
    : TenantBaseRepository<Table>(dataContext, tenantProvider), ITableRepository
{
    public IQueryable<Table> GetByIdsAsNoTracking(Guid[] ids)
    {
        return DbSet.Where(x => !x.Deleted).Where(x => ids.Contains(x.Id));
    }
}