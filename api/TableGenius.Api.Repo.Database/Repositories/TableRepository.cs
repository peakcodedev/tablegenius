using TableGenius.Api.Entities.Place;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Repo.Database.Repositories;

public class TableRepository(RepositoryContext dataContext) : TenantBaseRepository<Table>(dataContext), ITableRepository
{
}