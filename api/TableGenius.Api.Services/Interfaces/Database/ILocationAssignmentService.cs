using System;
using System.Collections.Generic;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface ILocationAssignmentService : IDatabaseService<LocationAssignment>
{
    IEnumerable<LocationAssignment> GetAllByLocationIdAsNoTracking(Guid locationId);
}