using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Reservations;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class ReservationAssignmentPresenter : BasePresenter<ReservationAssignmentRm, ReservationAssignment>,
    IReservationAssignmentPresenter
{
    private readonly IMapper _mapper;
    private readonly IReservationAssignmentService _reservationAssignmentService;

    public ReservationAssignmentPresenter(IMapper mapper,
        IReservationAssignmentService reservationAssignmentAssignmentService) : base(
        reservationAssignmentAssignmentService, mapper)
    {
        _reservationAssignmentService = reservationAssignmentAssignmentService;
        _mapper = mapper;
    }

    public ReservationAssignmentRm Add(ReservationAssignmentRm entity)
    {
        var model = _mapper.Map<ReservationAssignment>(entity);

        var result = _reservationAssignmentService.Add(model);
        return _mapper.Map<ReservationAssignmentRm>(result);
    }

    public IEnumerable<ReservationAssignmentRm> GetList()
    {
        var all = _reservationAssignmentService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<ReservationAssignment>, List<ReservationAssignmentRm>>(all);
        return returnMap;
    }

    public ReservationAssignmentRm Update(ReservationAssignmentRm entity)
    {
        var db = _mapper.Map<ReservationAssignmentRm, ReservationAssignment>(entity);
        var elem = _reservationAssignmentService.Update(db);
        return _mapper.Map<ReservationAssignment, ReservationAssignmentRm>(elem);
    }
}