using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class ProjectBoothsController : DefaultController
{
    private readonly IProjectBoothPresenter _projectBoothPresenter;
    private readonly IUserPresenter _userPresenter;

    public ProjectBoothsController(IProjectBoothPresenter projectBoothPresenter, IUserPresenter userPresenter)
    {
        _projectBoothPresenter = projectBoothPresenter;
        _userPresenter = userPresenter;
    }

    [Authorize("admin")]
    [HttpGet("export")]
    public JsonResult GetList()
    {
        var res = _projectBoothPresenter.GetList();
        return Json(new DataJsonResult<ProjectBoothRm>(200, "project booths successfully returned", res));
    }

    [HttpGet("own")]
    public JsonResult OwnProjectBooth()
    {
        var userId = GetUserId(_userPresenter);
        var res = _projectBoothPresenter.GetProjectBoothByUserId(userId);
        return Json(new SingleDataJsonResult<ProjectBoothRm>(200, "project booth successfully returned", res));
    }

    [HttpPatch("{Id}")]
    [DisableRequestSizeLimit]
    public JsonResult Update([FromRoute] Guid id, [FromBody] ProjectBoothRm projectBoothRm)
    {
        projectBoothRm.Id = id;
        var res = _projectBoothPresenter.Update(projectBoothRm);
        return Json(res != null
            ? new SingleDataJsonResult<ProjectBoothRm>(200, "project booth successfully returned", res)
            : new InfoJsonResult(500, "Error on updating project booth"));
    }

    [HttpPatch("{Id}/layout")]
    [DisableRequestSizeLimit]
    public async Task<JsonResult> UpdateLayout([FromRoute] Guid id, [FromForm] FileRm fileRm)
    {
        var fileStream = new MemoryStream();
        await fileRm.File.CopyToAsync(fileStream);
        var res = await _projectBoothPresenter.UploadFile(id, fileStream,
            fileRm.File.ContentType);
        return Json(res != null
            ? new SingleDataJsonResult<string>(200, "project booth layout successfully updated", res)
            : new InfoJsonResult(500, "Error on updating project booth layout "));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _projectBoothPresenter.GetById(id);
        return Json(new SingleDataJsonResult<ProjectBoothRm>(200, "project booth successfully returned", res));
    }

    [HttpGet("{id}/project")]
    public JsonResult GetByProject(Guid id)
    {
        var res = _projectBoothPresenter.GetProjectBoothByProjectId(id);
        return Json(new SingleDataJsonResult<ProjectBoothRm>(200, "project booth successfully returned", res));
    }
}