using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Repo.Database.Repositories;

public class LocationAssignmentRepository(RepositoryContext dataContext)
    : IndependentBaseRepository<LocationAssignment>(dataContext), ILocationAssignmentRepository
{
    public IEnumerable<Location> GetAllLocationsByMailAsNoTracking(string mail)
    {
        return GetAllAsNoTracking().Where(x => x.Mail == mail).Include(x => x.Location).Select(x => x.Location);
    }
}