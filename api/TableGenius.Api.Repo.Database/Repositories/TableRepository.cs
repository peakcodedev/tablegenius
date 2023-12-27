using TableGenius.Api.Entities.Place;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database.Repositories;

public class TableRepository(RepositoryContext dataContext, TenantProvider tenantProvider)
    : TenantBaseRepository<Table>(dataContext, tenantProvider), ITableRepository
{
}