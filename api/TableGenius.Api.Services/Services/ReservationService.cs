using System.Linq;
using TableGenius.Api.Entities.Reservations;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class ReservationService : DatabaseServiceTenantBase<Reservation>, IReservationService
{
    public ReservationService(IReservationRepository reservationRepository, IApplicationLogger logger) :
        base(logger)
    {
        Repository = reservationRepository;
    }

    public IQueryable<Reservation> GetAllUpcomingReservationsAsNoTracking()
    {
        return (Repository as IReservationRepository)?.GetAllUpcomingReservationsAsNoTracking();
    }
}