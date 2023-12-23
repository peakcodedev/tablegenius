using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class ProjectImagesController : DefaultController
{
    private readonly IProjectImagePresenter _projectImagePresenter;

    public ProjectImagesController(IProjectImagePresenter projectImagePresenter)
    {
        _projectImagePresenter = projectImagePresenter;
    }

    [HttpGet("{projectId}")]
    public JsonResult GetAllImagesOfProject(Guid projectId)
    {
        var res = _projectImagePresenter.GetAllImagesOfProject(projectId);
        return Json(
            new DataJsonResult<ProjectImageRm>(200, "project images successfully returned", res));
    }


    [HttpPost("{projectId}")]
    [DisableRequestSizeLimit]
    public async Task<JsonResult> AddImage([FromRoute] Guid projectId, [FromForm] FileRm fileRm)
    {
        var fileStream = new MemoryStream();
        await fileRm.File.CopyToAsync(fileStream);
        var res = await _projectImagePresenter.Add(projectId, fileStream,
            fileRm.File.ContentType);
        return Json(res != null
            ? new SingleDataJsonResult<ProjectImageRm>(200, "project image successfully updated", res)
            : new InfoJsonResult(500, "Error on updating project image "));
    }

    [HttpDelete("{id}")]
    public JsonResult DeleteProjectImage(Guid id)
    {
        var success = _projectImagePresenter.DeleteById(id, true);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted project image")
            : new InfoJsonResult(500, "Error on deleting project image"));
    }
}