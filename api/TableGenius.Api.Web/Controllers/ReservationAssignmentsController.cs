using System;
using Microsoft.AspNetCore.Mvc;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;

namespace TableGenius.Api.Web.Controllers;

public class ReservationAssignmentsController : DefaultController
{
    private readonly IReservationAssignmentPresenter _reservationAssignmentPresenter;

    public ReservationAssignmentsController(IReservationAssignmentPresenter reservationAssignmentPresenter)
    {
        _reservationAssignmentPresenter = reservationAssignmentPresenter;
    }


    [HttpPost]
    public JsonResult Add(ReservationAssignmentRm reservationAssignment)
    {
        var res = _reservationAssignmentPresenter.Add(reservationAssignment);
        return Json(res != null
            ? new SingleDataJsonResult<ReservationAssignmentRm>(200, "successfully added reservation assignment", res)
            : new InfoJsonResult(500, "Error on adding reservation assignment"));
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _reservationAssignmentPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted reservation assignment")
            : new InfoJsonResult(500, "Error on deleting reservation assignment"));
    }
}