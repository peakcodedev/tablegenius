﻿using TableGenius.Api.Entities.Reservations;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class ReservationService : DatabaseServiceBase<Reservation>, IReservationService
{
    public ReservationService(IReservationRepository reservationRepository, IApplicationLogger logger) :
        base(logger)
    {
        _repository = reservationRepository;
    }
}