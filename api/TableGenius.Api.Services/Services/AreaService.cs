using TableGenius.Api.Entities.Place;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class AreaService : DatabaseServiceBase<Area>, IAreaService
{
    public AreaService(IAreaRepository areaRepository, IApplicationLogger logger) :
        base(logger)
    {
        _repository = areaRepository;
    }
}