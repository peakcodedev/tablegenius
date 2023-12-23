using System;
using Microsoft.AspNetCore.Mvc;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;

namespace TableGenius.Api.Web.Controllers;

public class AreasController : DefaultController
{
    private readonly IAreaPresenter _areaPresenter;

    public AreasController(IAreaPresenter areaPresenter)
    {
        _areaPresenter = areaPresenter;
    }

    //[Authorize("admin")]
    [HttpGet]
    public JsonResult GetAllAreas()
    {
        var res = _areaPresenter.GetList();
        return Json(new DataJsonResult<AreaRm>(200, "areas successfully returned", res));
    }


    [HttpPost]
    public JsonResult Add(AreaRm area)
    {
        var res = _areaPresenter.Add(area);
        return Json(res != null
            ? new SingleDataJsonResult<AreaRm>(200, "successfully added area", res)
            : new InfoJsonResult(500, "Error on adding area"));
    }

    //[Authorize("admin")]
    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _areaPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted area")
            : new InfoJsonResult(500, "Error on deleting area"));
    }

    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] AreaRm areaRm)
    {
        areaRm.Id = id;
        var res = _areaPresenter.Update(areaRm);
        return Json(res != null
            ? new SingleDataJsonResult<AreaRm>(200, "area successfully returned", res)
            : new InfoJsonResult(500, "Error on updating area"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _areaPresenter.GetById(id);
        return Json(new SingleDataJsonResult<AreaRm>(200, "area successfully returned", res));
    }
}