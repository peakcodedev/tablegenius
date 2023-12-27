using System.Collections.Generic;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Repo.Database.Interfaces;

public interface ILocationAssignmentRepository : IIndependentBaseRepository<LocationAssignment>
{
    IEnumerable<Location> GetAllLocationsByMailAsNoTracking(string mail);
}