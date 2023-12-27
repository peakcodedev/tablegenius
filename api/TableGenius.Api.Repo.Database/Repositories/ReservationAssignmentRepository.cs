using TableGenius.Api.Entities.Reservations;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database.Repositories;

public class ReservationAssignmentRepository(RepositoryContext dataContext, TenantProvider tenantProvider)
    : TenantBaseRepository<ReservationAssignment>(dataContext, tenantProvider), IReservationAssignmentRepository
{
}