using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.General;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class ExportsController : DefaultController
{
    private readonly ICompanyPresenter _companyPresenter;
    private readonly ICoursePresenter _coursePresenter;
    private readonly IEmployeePresenter _employeePresenter;
    private readonly IMapper _mapper;
    private readonly IProfessionPresenter _professionPresenter;
    private readonly IProjectBoothPresenter _projectBoothPresenter;
    private readonly IProjectCollaborationPresenter _projectCollaborationPresenter;
    private readonly IProjectExpertPresenter _projectExpertPresenter;
    private readonly IProjectPresenter _projectPresenter;
    private readonly IProjectRatingPresenter _projectRatingPresenter;
    private readonly IProjectUserRatingPresenter _projectUserRatingPresenter;
    private readonly IUserPresenter _userPresenter;

    public ExportsController(ICompanyPresenter companyPresenter, IProjectPresenter projectPresenter,
        IProjectBoothPresenter projectBoothPresenter, IUserPresenter userPresenter,
        IProfessionPresenter professionPresenter, IProjectCollaborationPresenter projectCollaborationPresenter,
        IEmployeePresenter employeePresenter, IMapper mapper, ICoursePresenter coursePresenter,
        IProjectExpertPresenter projectExpertPresenter, IProjectRatingPresenter projectRatingPresenter,
        IProjectUserRatingPresenter projectUserRatingPresenter)
    {
        _userPresenter = userPresenter;
        _projectPresenter = projectPresenter;
        _projectBoothPresenter = projectBoothPresenter;
        _professionPresenter = professionPresenter;
        _companyPresenter = companyPresenter;
        _projectCollaborationPresenter = projectCollaborationPresenter;
        _employeePresenter = employeePresenter;
        _mapper = mapper;
        _coursePresenter = coursePresenter;
        _projectExpertPresenter = projectExpertPresenter;
        _projectUserRatingPresenter = projectUserRatingPresenter;
        _projectRatingPresenter = projectRatingPresenter;
    }

    [Authorize("admin")]
    [HttpGet("companies")]
    public JsonResult GetCompanies()
    {
        return Json(new DataJsonResult<CompanyRm>(200, "export successfully returned", _companyPresenter.GetList()));
    }

    [Authorize("admin")]
    [HttpGet("projects")]
    public JsonResult GetProjects()
    {
        return Json(new DataJsonResult<ProjectRm>(200, "export successfully returned", _projectPresenter.GetList()));
    }

    [Authorize("admin")]
    [HttpGet("professions")]
    public JsonResult GetProfessions()
    {
        return Json(
            new DataJsonResult<ProfessionRm>(200, "export successfully returned", _professionPresenter.GetList()));
    }

    [Authorize("admin")]
    [HttpGet("experts")]
    public JsonResult GetExperts()
    {
        return Json(
            new DataJsonResult<ProjectExpertRm>(200, "export successfully returned",
                _projectExpertPresenter.GetList()));
    }

    [Authorize("admin")]
    [HttpGet("activeProfessions")]
    public JsonResult GetActiveProfessions()
    {
        var activeProfessionsInUsers = _userPresenter.GetList().Select(x => x.Profession).Distinct();
        var activeProfessions =
            _professionPresenter.GetList().Where(x => activeProfessionsInUsers.Any(y => Guid.Parse(y) == x.Id));
        return Json(
            new DataJsonResult<ProfessionRm>(200, "export successfully returned", activeProfessions));
    }

    [Authorize("admin")]
    [HttpGet("booths")]
    public JsonResult GetProjectBooths()
    {
        return Json(
            new DataJsonResult<ExportProjectBoothRm>(200, "export successfully returned",
                _projectBoothPresenter.GetExportList()));
    }

    [Authorize("admin")]
    [HttpGet("courses")]
    public JsonResult GetCourses()
    {
        return Json(
            new DataJsonResult<ExportCourseRM>(200, "export successfully returned",
                _coursePresenter.GetExportList()));
    }

    [Authorize("admin")]
    [HttpGet("projectUserRatings")]
    public JsonResult GetProjectUserRatings()
    {
        return Json(
            new DataJsonResult<ExportProjectUserRatingRM>(200, "export successfully returned",
                _projectUserRatingPresenter.GetExport()));
    }

    [Authorize("admin")]
    [HttpGet("pricesJury")]
    public JsonResult GetPricesJury()
    {
        var exportItems = BuildExportPriceRmItems(false, _projectPresenter.GetJuryPrices());

        exportItems =
            new List<ExportPriceRM>(exportItems.OrderBy(et => et.ProjectName).ThenBy(et => et.LastName).ToArray());

        return Json(
            new DataJsonResult<ExportPriceRM>(200, "export successfully returned",
                exportItems));
    }

    [Authorize("admin")]
    [HttpGet("pricesUser")]
    public JsonResult GetPricesUser()
    {
        var exportItems = BuildExportPriceRmItems(true, _projectPresenter.GetList());

        exportItems =
            new List<ExportPriceRM>(exportItems.OrderBy(et => et.ProjectName).ThenBy(et => et.LastName).ToArray());

        return Json(
            new DataJsonResult<ExportPriceRM>(200, "export successfully returned",
                exportItems));
    }

    [Authorize("admin")]
    [HttpGet("certificates")]
    public JsonResult GetCertificates()
    {
        var projects = _projectPresenter.GetList().ToArray();
        var projectCollaborations = _projectCollaborationPresenter.GetAllNonDeletedEntries().ToArray();
        var users = _userPresenter.GetList().ToArray();
        var professions = _professionPresenter.GetList().ToArray();
        var exportItems = new List<ExportCertificateRM>();
        foreach (var project in projects)
        {
            var collaborationsOfProject = projectCollaborations.Where(x => x.ProjectId == project.Id).ToArray();
            foreach (var cp in collaborationsOfProject)
            {
                var user = users.SingleOrDefault(x => x.Id == cp.UserId);
                if (user == null) continue;
                var company = _companyPresenter.GetCompanyByUserId(user.Id);
                var projectRating = _projectRatingPresenter.GetProjectRatingByProjectId(project.Id);
                var item = new ExportCertificateRM
                {
                    Id = user.Id,
                    Profession = string.IsNullOrEmpty(user.Profession)
                        ? ""
                        : professions.Where(x => x.Id == Guid.Parse(user.Profession)).Select(x =>
                            user.Gender == Gender.Männlich ? x.MaleTitle : x.FemaleTitle).First(),
                    ApprenticeshipYear = user.ApprenticeshipYear,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Year = user.Year,
                    ProjectName = project.Title,
                    ProjectType = project.TypeString,
                    CompanyName = company?.Name,
                    Rating = projectRating?.Rating
                };
                exportItems.Add(item);
            }
        }

        exportItems =
            new List<ExportCertificateRM>(exportItems.OrderByDescending(et => et.Rating).ThenBy(et => et.ProjectName)
                .ToArray());

        return Json(
            new DataJsonResult<ExportCertificateRM>(200, "export successfully returned",
                exportItems));
    }

    [Authorize("admin")]
    [HttpGet("oldProjects")]
    public JsonResult GetOldProjects()
    {
        var projects = _projectPresenter.GetList().OrderBy(p => p.CreateDate).ToArray();
        var exportItems = new List<ExportPrintProjectRM>();
        var projectNumber = 1000;
        foreach (var project in projects)
        {
            var item = new ExportPrintProjectRM
            {
                projectid = projectNumber,
                projectdescription = project.Description,
                projecttitle = project.Title,
                projectpublic = 0,
                projectyear = project.Year,
                projectype = project.TypeString
            };
            exportItems.Add(item);
            projectNumber++;
        }

        return Json(
            new DataJsonResult<ExportPrintProjectRM>(200, "export successfully returned",
                exportItems));
    }

    [Authorize("admin")]
    [HttpGet("oldUsers")]
    public JsonResult GetOldUsers()
    {
        var projects = _projectPresenter.GetList().OrderBy(p => p.CreateDate).ToArray();
        var projectCollaborations = _projectCollaborationPresenter.GetAllNonDeletedEntries().ToArray();
        var users = _userPresenter.GetList().ToArray();
        var professions = _professionPresenter.GetList().ToArray();
        var exportItems = new List<ExportPrintUserRM>();
        var projectNumber = 1000;
        var userNumber = 2000;
        foreach (var project in projects)
        {
            var collaborationsOfProject = projectCollaborations.Where(x => x.ProjectId == project.Id).ToArray();
            foreach (var cp in collaborationsOfProject)
            {
                var user = users.SingleOrDefault(x => x.Id == cp.UserId);
                if (user == null) continue;
                var company = _companyPresenter.GetCompanyByUserId(user.Id);
                var item = new ExportPrintUserRM
                {
                    userid = userNumber,
                    userproject = projectNumber,
                    userprofession = string.IsNullOrEmpty(user.Profession)
                        ? ""
                        : professions.Where(x => x.Id == Guid.Parse(user.Profession)).Select(x =>
                            user.Gender == Gender.Männlich ? x.MaleTitle : x.FemaleTitle).First(),
                    usereducationalyear = user.ApprenticeshipYear + " Lehrjahr",
                    userfirstname = user.FirstName,
                    userlastname = user.LastName,
                    usercompanyname = company?.Name,
                    usercompanyimage = company == null || string.IsNullOrEmpty(company.Image)
                        ? ""
                        : "files/usercompanyimage/" + company.Image + ".jpg",
                    usercompanydescription = company?.Description,
                    userimage = string.IsNullOrEmpty(user.ProfileImage)
                        ? ""
                        : "files/userimage/" + user.ProfileImage + ".jpg"
                };
                exportItems.Add(item);
                userNumber++;
            }

            projectNumber++;
        }

        return Json(
            new DataJsonResult<ExportPrintUserRM>(200, "export successfully returned",
                exportItems));
    }

    [Authorize("admin")]
    [HttpGet("users")]
    public JsonResult GetUsers()
    {
        var projectCollaborations = _projectCollaborationPresenter.GetAllNonDeletedEntries().ToArray();
        var employees = _employeePresenter.GetAllNonDeletedEntries().ToArray();
        var users = _mapper.Map<IEnumerable<UserRM>, List<ExportUserRM>>(_userPresenter.GetList());
        foreach (var user in users)
        {
            var company = employees.SingleOrDefault(x => x.UserId == user.Id);
            var project = projectCollaborations.SingleOrDefault(x => x.UserId == user.Id);
            user.CompanyId = company != null ? company.CompanyId.ToString() : "";
            user.ProjectId = project != null ? project.ProjectId.ToString() : "";
        }

        return Json(
            new DataJsonResult<ExportUserRM>(200, "export successfully returned", users));
    }

    private IEnumerable<ExportPriceRM> BuildExportPriceRmItems(bool userPrice, IEnumerable<ProjectRm> projects)
    {
        var projectsCount = _projectPresenter.GetList().Count();
        var projectCollaborations = _projectCollaborationPresenter.GetAllNonDeletedEntries().ToArray();
        var users = _userPresenter.GetList().ToArray();
        var professions = _professionPresenter.GetList().ToArray();
        var exportItems = new List<ExportPriceRM>();
        var projectUserRatings = _projectUserRatingPresenter.GetExport().ToArray();
        foreach (var project in projects)
        {
            var collaborationsOfProject = projectCollaborations.Where(x => x.ProjectId == project.Id).ToArray();
            foreach (var cp in collaborationsOfProject)
            {
                var user = users.SingleOrDefault(x => x.Id == cp.UserId);
                if (user == null) continue;
                var company = _companyPresenter.GetCompanyByUserId(user.Id);
                var item = new ExportPriceRM
                {
                    Id = user.Id,
                    Profession = string.IsNullOrEmpty(user.Profession)
                        ? ""
                        : professions.Where(x => x.Id == Guid.Parse(user.Profession)).Select(x =>
                            user.Gender == Gender.Männlich ? x.MaleTitle : x.FemaleTitle).First(),
                    ApprenticeshipYear = user.ApprenticeshipYear,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Year = user.Year,
                    ProjectName = project.Title,
                    ProjectType = project.TypeString,
                    CompanyName = company?.Name,
                    ProjectCount = projectsCount
                };
                if (userPrice) item.PointsUserPrice = projectUserRatings.FirstOrDefault(x => x.Id == project.Id)!.Count;
                exportItems.Add(item);
            }
        }

        return exportItems;
    }
}