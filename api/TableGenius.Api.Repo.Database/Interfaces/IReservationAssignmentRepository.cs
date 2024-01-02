using System;
using System.Collections.Generic;
using TableGenius.Api.Entities.Reservations;

namespace TableGenius.Api.Repo.Database.Interfaces;

public interface IReservationAssignmentRepository : ITenantBaseRepository<ReservationAssignment>
{
    Guid[] GetAllAssignedTableIdsByAreaSlotAndCurrentDateAsNoTracking(Guid areaSlotId,
        DateTime dateTime);

    IEnumerable<ReservationAssignment> GetReservationAssignmentsByAreaSlotAndCurrentDate(Guid areaSlotId,
        DateTime dateTime);
}