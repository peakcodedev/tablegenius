using TableGenius.Api.Entities.Place;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class LocationService : DatabaseServiceBase<Location>, ILocationService
{
    public LocationService(ILocationRepository locationRepository, IApplicationLogger logger) :
        base(logger)
    {
        _repository = locationRepository;
    }
}