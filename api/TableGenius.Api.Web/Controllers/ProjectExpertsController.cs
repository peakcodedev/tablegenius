using System;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

[Authorize("admin")]
public class ProjectExpertsController : DefaultController
{
    private readonly IProjectExpertPresenter _projectExpertPresenter;

    public ProjectExpertsController(IProjectExpertPresenter projectExpertExpertPresenter)
    {
        _projectExpertPresenter = projectExpertExpertPresenter;
    }

    [HttpGet]
    public JsonResult GetList()
    {
        var res = _projectExpertPresenter.GetList();
        return Json(new DataJsonResult<ProjectExpertRm>(200, "project experts successfully returned", res));
    }

    [HttpPost]
    public JsonResult Add(ProjectExpertRm group)
    {
        var res = _projectExpertPresenter.Add(group);
        return Json(res != null
            ? new SingleDataJsonResult<ProjectExpertRm>(200, "successfully added project expert", res)
            : new InfoJsonResult(500, "Error on adding project expert"));
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _projectExpertPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted project expert")
            : new InfoJsonResult(500, "Error on deleting project expert"));
    }

    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] ProjectExpertRm projectExpertRM)
    {
        projectExpertRM.Id = id;
        var res = _projectExpertPresenter.Update(projectExpertRM);
        return Json(res != null
            ? new SingleDataJsonResult<ProjectExpertRm>(200, "project expert successfully returned", res)
            : new InfoJsonResult(500, "Error on updating project expert"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _projectExpertPresenter.GetById(id);
        return Json(new SingleDataJsonResult<ProjectExpertRm>(200, "project expert successfully returned", res));
    }
}