using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TableGenius.Api.Entities.Reservations;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database.Repositories;

public class ReservationAssignmentRepository(RepositoryContext dataContext, TenantProvider tenantProvider)
    : TenantBaseRepository<ReservationAssignment>(dataContext, tenantProvider), IReservationAssignmentRepository
{
    public Guid[] GetAllAssignedTableIdsByAreaSlotAndCurrentDateAsNoTracking(Guid areaSlotId,
        DateTime dateTime)
    {
        var tableReservationAssignments = DbSet.Include(x => x.TableReservationAssignments)
            .Where(o => !o.Deleted && o.AreaSlotId == areaSlotId && dateTime.Date == o.BookingDate.Date)
            .Select(x => x.TableReservationAssignments)
            .AsNoTracking().ToArray();
        var tableIds = tableReservationAssignments.SelectMany(x => x).Select(x => x.TableId);
        return tableIds.ToArray();
    }
}