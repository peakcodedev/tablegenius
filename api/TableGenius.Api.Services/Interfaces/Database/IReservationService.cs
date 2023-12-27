using System.Linq;
using TableGenius.Api.Entities.Reservations;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IReservationService : IDatabaseService<Reservation>
{
    IQueryable<Reservation> GetAllUpcomingReservationsAsNoTracking();
    IQueryable<Reservation> GetAllUpcomingAndUnassignedReservationsAsNoTracking();
}