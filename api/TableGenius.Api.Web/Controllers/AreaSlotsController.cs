using System;
using Microsoft.AspNetCore.Mvc;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.Models;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;

namespace TableGenius.Api.Web.Controllers;

public class AreaSlotsController : DefaultController
{
    private readonly IAreaSlotPresenter _areaSlotPresenter;
    private readonly IReservationAssignmentPresenter _reservationAssignmentPresenter;
    private readonly ITablePresenter _tablePresenter;

    public AreaSlotsController(IAreaSlotPresenter areaSlotPresenter, ITablePresenter tablePresenter,
        IReservationAssignmentPresenter reservationAssignmentPresenter)
    {
        _areaSlotPresenter = areaSlotPresenter;
        _tablePresenter = tablePresenter;
        _reservationAssignmentPresenter = reservationAssignmentPresenter;
    }

    [HttpGet]
    public JsonResult GetAll()
    {
        var res = _areaSlotPresenter.GetList();
        return Json(new DataJsonResult<AreaSlotRm>(200, "area slots successfully returned", res));
    }

    [HttpPost("{id}/assignedTables")]
    public JsonResult GetAllAssigendTablesByAreaSlotAndCurrentDate([FromBody] DateFilterModel model,
        [FromRoute] Guid id)
    {
        var res = _tablePresenter.GetAllAssignedTablesByAreaSlotAndCurrentDate(id, model.DateTime);
        return Json(new DataJsonResult<TableWithStatusRm>(200, "area slots successfully returned", res));
    }

    [HttpPost("{id}/freeTables")]
    public JsonResult GetFreeTables([FromBody] DateFilterModel model,
        [FromRoute] Guid id)
    {
        var res = _tablePresenter.GetFreeTablesByAreaSlotAndCurrentDate(id, model.DateTime);
        return Json(new DataJsonResult<TableRm>(200, "area slots successfully returned", res));
    }

    [HttpPost("{id}/reservations")]
    public JsonResult GetReservationsByAreaSlotAndDate([FromBody] DateFilterModel model,
        [FromRoute] Guid id)
    {
        var res = _reservationAssignmentPresenter.GetReservationAssignmentsByAreaSlotAndCurrentDate(id,
            model.DateTime);
        return Json(
            new DataJsonResult<ReservationAssignmentInfoRm>(200, "reservation assignments successfully returned", res));
    }


    [HttpPost]
    public JsonResult Add(AreaSlotRm table)
    {
        var res = _areaSlotPresenter.Add(table);
        return Json(res != null
            ? new SingleDataJsonResult<AreaSlotRm>(200, "successfully added area slot", res)
            : new InfoJsonResult(500, "Error on adding area slot"));
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _areaSlotPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted area slot")
            : new InfoJsonResult(500, "Error on deleting area slot"));
    }

    [HttpPatch("{id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] AreaSlotRm areaSlotRm)
    {
        areaSlotRm.Id = id;
        var res = _areaSlotPresenter.Update(areaSlotRm);
        return Json(res != null
            ? new SingleDataJsonResult<AreaSlotRm>(200, "area slot successfully returned", res)
            : new InfoJsonResult(500, "Error on updating area slot"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _areaSlotPresenter.GetById(id);
        return Json(new SingleDataJsonResult<AreaSlotRm>(200, "area slot successfully returned", res));
    }
}