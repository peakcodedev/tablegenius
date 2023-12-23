using System;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class ProjectCollaboratorsController : DefaultController
{
    private readonly IProjectCollaborationPresenter _projectCollaborationPresenter;

    public ProjectCollaboratorsController(IProjectCollaborationPresenter projectPresenter)
    {
        _projectCollaborationPresenter = projectPresenter;
    }

    [HttpGet("{projectId}")]
    public JsonResult GetCollaboratorsOfProject(Guid projectId)
    {
        var res = _projectCollaborationPresenter.GetList(projectId);
        return Json(
            new DataJsonResult<ProjectCollaborationRm>(200, "project collaborations successfully returned", res));
    }


    [HttpPost("{projectId}")]
    public JsonResult AddEmployee([FromBody] ProjectCollaborationModel projectCollaboration, [FromRoute] Guid projectId)
    {
        projectCollaboration.ProjectId = projectId;
        var res = _projectCollaborationPresenter.Add(projectCollaboration);
        return Json(res != null
            ? new SingleDataJsonResult<ProjectCollaborationRm>(200, "successfully added project collaborator", res)
            : new InfoJsonResult(500, "Error on adding employee"));
    }

    [HttpDelete("{id}")]
    public JsonResult DeleteProjectCollaboration(Guid id)
    {
        var success = _projectCollaborationPresenter.DeleteById(id, true);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted project collaboration")
            : new InfoJsonResult(500, "Error on deleting project collaboration"));
    }
}