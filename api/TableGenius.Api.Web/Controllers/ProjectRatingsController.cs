using System;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

[Authorize("admin")]
public class ProjectRatingsController : DefaultController
{
    private readonly IProjectRatingPresenter _projectRatingPresenter;

    public ProjectRatingsController(IProjectRatingPresenter projectRatingPresenter)
    {
        _projectRatingPresenter = projectRatingPresenter;
    }

    [HttpGet]
    public JsonResult GetList()
    {
        var res = _projectRatingPresenter.GetList();
        return Json(new DataJsonResult<ProjectRatingRm>(200, "project ratings successfully returned", res));
    }

    [HttpPost]
    public JsonResult Add(ProjectRatingRm projectRating)
    {
        var res = _projectRatingPresenter.Add(projectRating);
        return Json(res != null
            ? new SingleDataJsonResult<ProjectRatingRm>(200, "successfully added project rating", res)
            : new InfoJsonResult(500, "Error on adding project rating"));
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _projectRatingPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted project rating")
            : new InfoJsonResult(500, "Error on deleting project rating"));
    }

    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] ProjectRatingRm projectRatingRm)
    {
        projectRatingRm.Id = id;
        var res = _projectRatingPresenter.Update(projectRatingRm);
        return Json(res != null
            ? new SingleDataJsonResult<ProjectRatingRm>(200, "project rating successfully returned", res)
            : new InfoJsonResult(500, "Error on updating project rating"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _projectRatingPresenter.GetById(id);
        return Json(new SingleDataJsonResult<ProjectRatingRm>(200, "project rating successfully returned", res));
    }

    [HttpGet("{projectId}/project")]
    public JsonResult GetByProject(Guid projectId)
    {
        var res = _projectRatingPresenter.GetProjectRatingByProjectId(projectId);
        return Json(new SingleDataJsonResult<ProjectRatingRm>(200, "project rating successfully returned", res));
    }
}