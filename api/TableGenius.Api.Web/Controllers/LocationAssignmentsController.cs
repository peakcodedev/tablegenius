using System;
using Microsoft.AspNetCore.Mvc;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;

namespace TableGenius.Api.Web.Controllers;

public class LocationAssignmentsController : DefaultController
{
    private readonly ILocationAssignmentPresenter _locationAssignmentPresenter;

    public LocationAssignmentsController(ILocationAssignmentPresenter locationAssignmentPresenter)
    {
        _locationAssignmentPresenter = locationAssignmentPresenter;
    }

    [HttpGet("{LocationId}")]
    public JsonResult GetAllAssignmentsByLocation([FromRoute] Guid locationId)
    {
        var res = _locationAssignmentPresenter.GetListByLocation(locationId);
        return Json(new DataJsonResult<LocationAssignmentRm>(200, "location assignments successfully returned", res));
    }


    [HttpPost]
    public JsonResult Add(LocationAssignmentRm location)
    {
        var res = _locationAssignmentPresenter.Add(location);
        return Json(res != null
            ? new SingleDataJsonResult<LocationAssignmentRm>(200, "successfully added location assignment", res)
            : new InfoJsonResult(500, "Error on adding location assignment"));
    }

    //[Authorize("admin")]
    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _locationAssignmentPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted location assignment")
            : new InfoJsonResult(500, "Error on deleting location assignment"));
    }

    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] LocationAssignmentRm locationAssignmentRm)
    {
        locationAssignmentRm.Id = id;
        var res = _locationAssignmentPresenter.Update(locationAssignmentRm);
        return Json(res != null
            ? new SingleDataJsonResult<LocationAssignmentRm>(200, "location assignment successfully returned", res)
            : new InfoJsonResult(500, "Error on updating location assignment"));
    }
}