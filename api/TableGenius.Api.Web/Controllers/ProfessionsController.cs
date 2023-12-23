using System;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class ProfessionsController : DefaultController
{
    private readonly IProfessionPresenter _professionPresenter;

    public ProfessionsController(IProfessionPresenter professionPresenter)
    {
        _professionPresenter = professionPresenter;
    }

    [HttpGet]
    public JsonResult GetList()
    {
        var res = _professionPresenter.GetList();
        return Json(new DataJsonResult<ProfessionRm>(200, "professions successfully returned", res));
    }

    [Authorize("admin")]
    [HttpPost]
    public JsonResult Add(ProfessionRm profession)
    {
        var res = _professionPresenter.Add(profession);
        return Json(res != null
            ? new SingleDataJsonResult<ProfessionRm>(200, "successfully added professions", res)
            : new InfoJsonResult(500, "Error on adding professions"));
    }

    [Authorize("admin")]
    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _professionPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted profession")
            : new InfoJsonResult(500, "Error on deleting profession"));
    }

    [Authorize("admin")]
    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] ProfessionRm professionRm)
    {
        professionRm.Id = id;
        var res = _professionPresenter.Update(professionRm);
        return Json(res != null
            ? new SingleDataJsonResult<ProfessionRm>(200, "profession successfully returned", res)
            : new InfoJsonResult(500, "Error on updating profession"));
    }

    [Authorize("admin")]
    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _professionPresenter.GetById(id);
        return Json(new SingleDataJsonResult<ProfessionRm>(200, "profession successfully returned", res));
    }
}