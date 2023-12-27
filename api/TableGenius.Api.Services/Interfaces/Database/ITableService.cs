using System;
using System.Linq;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface ITableService : IDatabaseService<Table>
{
    IQueryable<Table> GetAllAssignedTablesByAreaSlotAndCurrentDateAsNoTracking(Guid areaSlotId, DateTime dateTime);
}