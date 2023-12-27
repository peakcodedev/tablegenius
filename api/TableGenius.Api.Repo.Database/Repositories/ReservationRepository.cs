using System;
using System.Linq;
using TableGenius.Api.Entities.Reservations;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database.Repositories;

public class ReservationRepository(RepositoryContext dataContext, TenantProvider tenantProvider)
    : TenantBaseRepository<Reservation>(dataContext, tenantProvider), IReservationRepository
{
    public IQueryable<Reservation> GetAllUpcomingReservationsAsNoTracking()
    {
        var beginningCurrentDay = DateTime.Today;
        return GetAllAsNoTracking().Where(x => x.BookingDate >= beginningCurrentDay);
    }
}