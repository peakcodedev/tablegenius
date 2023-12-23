using System;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class ProjectUserRatingsController : DefaultController
{
    private readonly IProjectUserRatingPresenter _projectUserRatingPresenter;
    private readonly IUserPresenter _userPresenter;

    public ProjectUserRatingsController(IProjectUserRatingPresenter projectUserRatingPresenter,
        IUserPresenter userPresenter)
    {
        _projectUserRatingPresenter = projectUserRatingPresenter;
        _userPresenter = userPresenter;
    }

    [HttpGet("own")]
    public JsonResult GetOwnProjectUserRating()
    {
        var userId = GetUserId(_userPresenter);
        var res = _projectUserRatingPresenter.GetByUserId(userId);
        return Json(
            new SingleDataJsonResult<ProjectUserRatingRM>(200, "project user rating successfully returned", res));
    }


    [HttpPost]
    public JsonResult AddProjectUserRating(ProjectUserRatingRM projectUserRating)
    {
        var res = _projectUserRatingPresenter.Add(projectUserRating);

        return Json(res != null
            ? new SingleDataJsonResult<ProjectUserRatingRM>(200, "successfully added project user rating", res)
            : new InfoJsonResult(500, "Error on adding project user rating"));
    }

    [Authorize("admin")]
    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _projectUserRatingPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted project user rating")
            : new InfoJsonResult(500, "Error on deleting project user rating"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _projectUserRatingPresenter.GetById(id);
        return Json(
            new SingleDataJsonResult<ProjectUserRatingRM>(200, "project user rating successfully returned", res));
    }
}