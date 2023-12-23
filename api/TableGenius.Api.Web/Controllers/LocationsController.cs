using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class LocationsController : DefaultController
{
    private readonly ILocationPresenter _locationPresenter;

    public LocationsController(ILocationPresenter locationPresenter)
    {
        _locationPresenter = locationPresenter;
    }

    //[Authorize("admin")]
    [HttpGet]
    public JsonResult GetAllLocations()
    {
        var res = _locationPresenter.GetList();
        return Json(new DataJsonResult<LocationRM>(200, "locations successfully returned", res));
    }


    [HttpPost]
    public JsonResult Add(LocationRM location)
    {
        var res = _locationPresenter.Add(location);
        return Json(res != null
            ? new SingleDataJsonResult<LocationRM>(200, "successfully added location", res)
            : new InfoJsonResult(500, "Error on adding location"));
    }

    //[Authorize("admin")]
    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _locationPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted location")
            : new InfoJsonResult(500, "Error on deleting location"));
    }

    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] LocationRM locationRm)
    {
        locationRm.Id = id;
        var res = _locationPresenter.Update(locationRm);
        return Json(res != null
            ? new SingleDataJsonResult<LocationRM>(200, "location successfully returned", res)
            : new InfoJsonResult(500, "Error on updating location"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _locationPresenter.GetById(id);
        return Json(new SingleDataJsonResult<LocationRM>(200, "location successfully returned", res));
    }
}