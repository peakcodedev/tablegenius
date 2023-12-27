using System;
using System.Collections.Generic;
using System.Linq;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class LocationAssignmentService : DatabaseServiceBase<LocationAssignment>, ILocationAssignmentService
{
    public LocationAssignmentService(ILocationAssignmentRepository locationAssignmentRepository,
        IApplicationLogger logger) :
        base(logger)
    {
        _repository = locationAssignmentRepository;
    }

    public IEnumerable<LocationAssignment> GetAllByLocationIdAsNoTracking(Guid locationId)
    {
        return _repository.GetAllAsNoTracking().Where(x => x.LocationId == locationId).ToList();
    }
}