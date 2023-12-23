using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Repo.Database.Repositories;

public class LocationRepository(RepositoryContext dataContext) : BaseRepository<Location>(dataContext), ILocationRepository
{
    
}