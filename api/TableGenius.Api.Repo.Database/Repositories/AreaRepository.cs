using TableGenius.Api.Entities.Place;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Repo.Database.Repositories;

public class AreaRepository(RepositoryContext dataContext) : TenantBaseRepository<Area>(dataContext), IAreaRepository
{
}