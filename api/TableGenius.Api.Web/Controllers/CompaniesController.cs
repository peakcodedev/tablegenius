using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class CompaniesController : DefaultController
{
    private readonly ICompanyPresenter _companyPresenter;
    private readonly IUserPresenter _userPresenter;

    public CompaniesController(ICompanyPresenter companyPresenter, IUserPresenter userPresenter)
    {
        _companyPresenter = companyPresenter;
        _userPresenter = userPresenter;
    }

    [Authorize("admin")]
    [HttpGet]
    public JsonResult GetList()
    {
        var res = _companyPresenter.GetList();
        return Json(new DataJsonResult<CompanyRm>(200, "companies successfully returned", res));
    }

    [HttpGet("own")]
    public JsonResult GetOwnCompany()
    {
        var userId = GetUserId(_userPresenter);
        var res = _companyPresenter.GetCompanyByUserId(userId);
        return Json(new SingleDataJsonResult<CompanyRm>(200, "company successfully returned", res));
    }


    [HttpPost]
    public JsonResult Add(CompanyRm company)
    {
        var addCollaborationToCompany = !isAdmin();
        var res = _companyPresenter.Add(company);
        if (addCollaborationToCompany && res.Id != Guid.Empty)
        {
            var employee = _companyPresenter.AddEmployeeToCompany(GetUserId(_userPresenter), res.Id);
        }

        return Json(res != null
            ? new SingleDataJsonResult<CompanyRm>(200, "successfully added company", res)
            : new InfoJsonResult(500, "Error on adding company"));
    }

    [Authorize("admin")]
    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _companyPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted company")
            : new InfoJsonResult(500, "Error on deleting company"));
    }

    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] CompanyRm companyRm)
    {
        companyRm.Id = id;
        var res = _companyPresenter.Update(companyRm);
        return Json(res != null
            ? new SingleDataJsonResult<CompanyRm>(200, "company successfully returned", res)
            : new InfoJsonResult(500, "Error on updating company"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _companyPresenter.GetById(id);
        return Json(new SingleDataJsonResult<CompanyRm>(200, "company successfully returned", res));
    }

    [HttpPatch("{id}/Picture")]
    [DisableRequestSizeLimit]
    public async Task<JsonResult> UploadCompanyPicture([FromForm] CompanyPictureRm companyPictureRm)
    {
        var pictureMemoryStream = new MemoryStream();
        await companyPictureRm.File.CopyToAsync(pictureMemoryStream);
        var res = await _companyPresenter.UploadCompanyPicture(companyPictureRm.CompanyId, pictureMemoryStream,
            companyPictureRm.File.ContentType);
        return Json(res != null
            ? new SingleDataJsonResult<string>(200, "company picture successfully updated", res)
            : new InfoJsonResult(500, "Error on updating company picture"));
    }
}