using System;
using System.Linq;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Repo.Database.Interfaces;

public interface ITableRepository : ITenantBaseRepository<Table>
{
    IQueryable<Table> GetByIdsAsNoTracking(Guid[] ids);
}