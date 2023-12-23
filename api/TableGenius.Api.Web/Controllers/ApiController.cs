using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Settings;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace TableGenius.Api.Web.Controllers;

[EnableCors("_myAllowSpecificOrigins")]
[ApiController]
public class ApiController : Controller
{
    private readonly ICompanyPresenter _companyPresenter;
    private readonly GeneralSettings _generalSettings;
    private readonly IProjectPresenter _projectPresenter;
    private readonly IUserPresenter _userPresenter;

    public ApiController(IProjectPresenter projectPresenter, IUserPresenter userPresenter,
        IOptions<GeneralSettings> settings, ICompanyPresenter companyPresenter)
    {
        _projectPresenter = projectPresenter;
        _userPresenter = userPresenter;
        _generalSettings = settings.Value;
        _companyPresenter = companyPresenter;
    }

    [HttpGet("private/projects")]
    public ActionResult GetProjects([FromQuery] string password)
    {
        if (!IsAuthenticated(password)) return Forbid();

        var res = _projectPresenter.GetList();
        return Json(new DataJsonResult<ProjectRm>(200, "projects successfully returned", res));
    }

    [HttpGet("private/users")]
    public ActionResult GetUsers([FromQuery] string password)
    {
        if (!IsAuthenticated(password)) return Forbid();

        var res = _userPresenter.GetList();
        return Json(new DataJsonResult<UserRM>(200, "users successfully returned", res));
    }

    [HttpGet("private/companies")]
    public ActionResult GetCompanies([FromQuery] string password)
    {
        if (!IsAuthenticated(password)) return Forbid();

        var res = _companyPresenter.GetList();
        return Json(new DataJsonResult<CompanyRm>(200, "companies successfully returned", res));
    }

    private bool IsAuthenticated(string password)
    {
        if (string.IsNullOrEmpty(password)) return false;
        return _generalSettings.ApiKey == password;
    }
}