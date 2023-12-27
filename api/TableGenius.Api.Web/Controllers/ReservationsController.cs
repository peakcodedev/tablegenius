using System;
using Microsoft.AspNetCore.Mvc;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;

namespace TableGenius.Api.Web.Controllers;

public class ReservationsController : DefaultController
{
    private readonly IReservationPresenter _reservationPresenter;

    public ReservationsController(IReservationPresenter reservationPresenter)
    {
        _reservationPresenter = reservationPresenter;
    }

    //[Authorize("admin")]
    [HttpGet]
    public JsonResult GetAll()
    {
        var res = _reservationPresenter.GetList();
        return Json(new DataJsonResult<ReservationRm>(200, "reservations successfully returned", res));
    }

    [HttpGet("unassigned")]
    public JsonResult GetAllUnassigned()
    {
        var res = _reservationPresenter.GetAllUnassignedList();
        return Json(new DataJsonResult<ReservationRm>(200, "reservations successfully returned", res));
    }


    [HttpPost]
    public JsonResult Add(ReservationRm reservation)
    {
        var res = _reservationPresenter.Add(reservation);
        return Json(res != null
            ? new SingleDataJsonResult<ReservationRm>(200, "successfully added reservation", res)
            : new InfoJsonResult(500, "Error on adding reservation"));
    }

    //[Authorize("admin")]
    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _reservationPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted reservation")
            : new InfoJsonResult(500, "Error on deleting reservation"));
    }

    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] ReservationRm reservationRm)
    {
        reservationRm.Id = id;
        var res = _reservationPresenter.Update(reservationRm);
        return Json(res != null
            ? new SingleDataJsonResult<ReservationRm>(200, "reservation successfully returned", res)
            : new InfoJsonResult(500, "Error on updating reservation"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _reservationPresenter.GetById(id);
        return Json(new SingleDataJsonResult<ReservationRm>(200, "reservation successfully returned", res));
    }
}