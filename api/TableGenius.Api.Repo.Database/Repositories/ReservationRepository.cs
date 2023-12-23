using TableGenius.Api.Entities.Reservations;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Repo.Database.Repositories;

public class ReservationRepository(RepositoryContext dataContext)
    : BaseRepository<Reservation>(dataContext), IReservationRepository
{
}