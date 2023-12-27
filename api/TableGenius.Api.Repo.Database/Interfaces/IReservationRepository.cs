using System.Linq;
using TableGenius.Api.Entities.Reservations;

namespace TableGenius.Api.Repo.Database.Interfaces;

public interface IReservationRepository : ITenantBaseRepository<Reservation>
{
    IQueryable<Reservation> GetAllUpcomingReservationsAsNoTracking();
    IQueryable<Reservation> GetAllUpcomingAndUnassignedReservationsAsNoTracking();
}