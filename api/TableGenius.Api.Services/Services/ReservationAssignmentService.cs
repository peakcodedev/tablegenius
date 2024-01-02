using System;
using System.Collections.Generic;
using TableGenius.Api.Entities.Reservations;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Repo.Database.Providers;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class ReservationAssignmentService : DatabaseServiceTenantBase<ReservationAssignment>,
    IReservationAssignmentService
{
    private readonly TenantProvider _tenantProvider;

    public ReservationAssignmentService(IReservationAssignmentRepository reservationAssignmentRepository,
        IApplicationLogger logger, TenantProvider tenantProvider) :
        base(logger)
    {
        Repository = reservationAssignmentRepository;
        _tenantProvider = tenantProvider;
    }

    public new ReservationAssignment Add(ReservationAssignment entity)
    {
        foreach (var tableReservationAssignment in entity.TableReservationAssignments)
            tableReservationAssignment.TenantId = _tenantProvider.GetTenantId();
        return base.Add(entity);
    }

    public Guid[] GetAllAssignedTableIdsByAreaSlotAndCurrentDateAsNoTracking(Guid areaSlotId, DateTime dateTime)
    {
        return (Repository as IReservationAssignmentRepository)
            ?.GetAllAssignedTableIdsByAreaSlotAndCurrentDateAsNoTracking(areaSlotId, dateTime);
    }

    public IEnumerable<ReservationAssignment> GetReservationAssignmentsByAreaSlotAndCurrentDate(Guid areaSlotId,
        DateTime dateTime)
    {
        return (Repository as IReservationAssignmentRepository)
            ?.GetReservationAssignmentsByAreaSlotAndCurrentDate(areaSlotId, dateTime);
    }
}