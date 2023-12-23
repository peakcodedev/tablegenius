using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Reservations;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class ReservationPresenter : BasePresenter<ReservationRm, Reservation>, IReservationPresenter
{
    private readonly IMapper _mapper;
    private readonly IReservationService _reservationService;

    public ReservationPresenter(IMapper mapper, IReservationService reservationService) : base(
        reservationService, mapper)
    {
        _reservationService = reservationService;
        _mapper = mapper;
    }

    public ReservationRm Add(ReservationRm entity)
    {
        var model = _mapper.Map<Reservation>(entity);
        var result = _reservationService.Add(model);
        return _mapper.Map<ReservationRm>(result);
    }

    public IEnumerable<ReservationRm> GetList()
    {
        var all = _reservationService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<Reservation>, List<ReservationRm>>(all);
        return returnMap;
    }

    public ReservationRm Update(ReservationRm entity)
    {
        var db = _mapper.Map<ReservationRm, Reservation>(entity);
        var elem = _reservationService.Update(db);
        return _mapper.Map<Reservation, ReservationRm>(elem);
    }
}