using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class ProjectsController : DefaultController
{
    private readonly IProjectPresenter _projectPresenter;
    private readonly IUserPresenter _userPresenter;

    public ProjectsController(IProjectPresenter projectPresenter, IUserPresenter userPresenter)
    {
        _projectPresenter = projectPresenter;
        _userPresenter = userPresenter;
    }

    [HttpGet]
    public JsonResult GetList()
    {
        var res = _projectPresenter.GetList();
        return Json(new DataJsonResult<ProjectRm>(200, "projects successfully returned", res));
    }

    [HttpGet("own")]
    public JsonResult GetOwnCompany()
    {
        var userId = GetUserId(_userPresenter);
        var res = _projectPresenter.GetProjectByUserId(userId);
        return Json(new SingleDataJsonResult<ProjectRm>(200, "project successfully returned", res));
    }


    [HttpPost]
    public JsonResult Add(ProjectRm project)
    {
        var addCollaborationToProject = !isAdmin();
        var res = _projectPresenter.Add(project, addCollaborationToProject ? GetUserId(_userPresenter) : null);

        return Json(res != null
            ? new SingleDataJsonResult<ProjectRm>(200, "successfully added project", res)
            : new InfoJsonResult(500, "Error on adding project"));
    }

    [Authorize("admin")]
    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _projectPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted company")
            : new InfoJsonResult(500, "Error on deleting company"));
    }

    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] ProjectRm projectRm)
    {
        projectRm.Id = id;
        var res = _projectPresenter.Update(projectRm, isAdmin());
        return Json(res != null
            ? new SingleDataJsonResult<ProjectRm>(200, "project successfully returned", res)
            : new InfoJsonResult(500, "Error on updating project"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _projectPresenter.GetById(id);
        return Json(new SingleDataJsonResult<ProjectRm>(200, "project successfully returned", res));
    }

    [HttpPatch("{Id}/file")]
    [DisableRequestSizeLimit]
    public async Task<JsonResult> UpdateFile([FromRoute] Guid id, [FromForm] FileRm fileRm)
    {
        var fileStream = new MemoryStream();
        await fileRm.File.CopyToAsync(fileStream);
        var res = await _projectPresenter.UploadFile(id, fileStream,
            fileRm.File.ContentType);
        return Json(res != null
            ? new SingleDataJsonResult<string>(200, "project documentation successfully updated", res)
            : new InfoJsonResult(500, "Error on updating project documentation"));
    }
}