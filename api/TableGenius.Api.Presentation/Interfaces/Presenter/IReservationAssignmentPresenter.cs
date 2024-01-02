using System;
using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IReservationAssignmentPresenter : IPresenter<ReservationAssignmentRm>
{
    ReservationAssignmentRm Add(ReservationAssignmentRm entity);
    IEnumerable<ReservationAssignmentRm> GetList();
    ReservationAssignmentRm Update(ReservationAssignmentRm entity);

    IEnumerable<ReservationAssignmentInfoRm> GetReservationAssignmentsByAreaSlotAndCurrentDate(Guid areaSlotId,
        DateTime dateTime);
}