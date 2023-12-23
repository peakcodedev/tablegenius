using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IReservationPresenter : IPresenter<ReservationRm>
{
    ReservationRm Add(ReservationRm entity);
    IEnumerable<ReservationRm> GetList();
    ReservationRm Update(ReservationRm entity);
}