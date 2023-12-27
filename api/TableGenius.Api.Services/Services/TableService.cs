using System;
using System.Linq;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class TableService : DatabaseServiceTenantBase<Table>, ITableService
{
    private readonly IReservationAssignmentRepository _reservationAssignmentRepository;

    public TableService(ITableRepository tableRepository, IApplicationLogger logger,
        IReservationAssignmentRepository reservationAssignmentRepository) :
        base(logger)
    {
        Repository = tableRepository;
        _reservationAssignmentRepository = reservationAssignmentRepository;
    }

    public IQueryable<Table> GetAllAssignedTablesByAreaSlotAndCurrentDateAsNoTracking(Guid areaSlotId,
        DateTime dateTime)
    {
        var ids =
            _reservationAssignmentRepository.GetAllAssignedTableIdsByAreaSlotAndCurrentDateAsNoTracking(areaSlotId,
                dateTime);
        return ((ITableRepository) Repository).GetByIdsAsNoTracking(ids);
    }
}