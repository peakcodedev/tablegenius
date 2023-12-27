using TableGenius.Api.Entities.Reservations;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IReservationAssignmentService : IDatabaseService<ReservationAssignment>
{
    new ReservationAssignment Add(ReservationAssignment entity);
}