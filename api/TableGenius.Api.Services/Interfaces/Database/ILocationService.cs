using System.Collections.Generic;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface ILocationService : IDatabaseService<Location>
{
    IEnumerable<Location> GetAllLocationsByMailAsNoTracking(string mail);
}