using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class UserPresenter : BasePresenter<UserRM, User>, IUserPresenter
{
    private readonly ICompanyService _companyService;
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;

    public UserPresenter(IMapper mapper, IUserService userService, ICompanyService companyService,
        IProjectService projectService) : base(userService, mapper)
    {
        _userService = userService;
        _mapper = mapper;
        _companyService = companyService;
        _projectService = projectService;
    }

    public override UserRM GetBlank()
    {
        return new UserRM();
    }

    public override void UpdateBlank(UserRM entity)
    {
        // NOTHING TO DO HERE
    }

    public UserRM Update(UserRM entity)
    {
        var existingUser = _userService.GetByMail(entity.Mail);
        if (entity.Id == Guid.Empty) entity.Id = existingUser.Id;
        entity.ProfileImage = existingUser.ProfileImage;
        var db = _mapper.Map<UserRM, User>(entity);
        var elem = _userService.Update(db);
        return _mapper.Map<User, UserRM>(elem);
    }

    public UserRM Add(UserRM entity)
    {
        var model = _mapper.Map<User>(entity);
        model.ProfileImage = null;
        var result = _userService.Add(model);
        return _mapper.Map<UserRM>(result);
    }

    public UserRM GetByMail(string mail)
    {
        var user = _userService.GetByMail(mail.Trim());
        return _mapper.Map<UserRM>(user);
    }

    public IEnumerable<UserRM> GetList()
    {
        var all = _userService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<User>, List<UserRM>>(all);
        return returnMap;
    }

    public UserRelationsRm GetUserRelations(Guid userId)
    {
        var company = _mapper.Map<Company, CompanyRm>(_companyService.GetByUserId(userId));
        var project = _mapper.Map<Project, ProjectRm>(_projectService.GetByUserId(userId));
        return new UserRelationsRm
        {
            Company = company,
            Project = project,
            ProjectId = project != null ? project.Id.ToString() : "",
            CompanyId = company != null ? company.Id.ToString() : ""
        };
    }

    public Task<string> UploadProfilePicture(Guid userId, MemoryStream profileImageStream, string contentType)
    {
        return _userService.UploadUserProfilePicture(userId, profileImageStream, contentType);
    }
}