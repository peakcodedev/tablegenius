using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

    public IQueryable<Reservation> GetAllUpcomingAndUnassignedReservationsAsNoTracking()
    {
        var beginningCurrentDay = DateTime.Today;
        return DbSet.Where(o => !o.Deleted).Include(x => x.ReservationAssignment)
            .Where(x => x.BookingDate >= beginningCurrentDay && x.ReservationAssignment == null).AsNoTracking();
    }
}