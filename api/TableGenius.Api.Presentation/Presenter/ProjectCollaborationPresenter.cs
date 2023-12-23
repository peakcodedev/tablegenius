using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class ProjectCollaborationPresenter : BasePresenter<ProjectCollaborationRm, ProjectCollaboration>,
    IProjectCollaborationPresenter
{
    private readonly IMapper _mapper;
    private readonly IProjectCollaborationService _projectCollaborationService;
    private readonly IUserService _userService;

    public ProjectCollaborationPresenter(IMapper mapper, IUserService userService,
        IProjectCollaborationService projectCollaborationService) : base(
        projectCollaborationService, mapper)
    {
        _userService = userService;
        _projectCollaborationService = projectCollaborationService;
        _mapper = mapper;
    }

    public IEnumerable<ProjectCollaborationRm> GetList(Guid projectId)
    {
        var all = _projectCollaborationService.GetAllAsNoTracking().Where(pc => pc.ProjectId == projectId).ToList();
        if (!all.Any()) return Array.Empty<ProjectCollaborationRm>();

        return (from projectCollaboration in all
            let user = _userService.GetById(projectCollaboration.UserId)
            where user != null
            select new ProjectCollaborationRm
            {
                Id = projectCollaboration.Id,
                ProjectId = projectCollaboration.ProjectId,
                UserId = projectCollaboration.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mail = user.Mail
            }).ToList();
    }

    public ProjectCollaborationRm Add(ProjectCollaborationModel entity)
    {
        var model = _mapper.Map<ProjectCollaboration>(entity);
        var existingUser = _userService.GetIdByMail(entity.Mail);
        if (existingUser == Guid.Empty) return null;
        model.UserId = existingUser;
        FindAndDeleteExistingCollaboration(model);
        var result = _projectCollaborationService.Add(model);
        var user = _userService.GetById(result.UserId);
        if (user == null) return null;
        var rm = new ProjectCollaborationRm
        {
            Id = result.Id,
            ProjectId = result.ProjectId,
            UserId = result.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Mail = user.Mail
        };
        return rm;
    }

    public override ProjectCollaborationRm GetBlank()
    {
        return new ProjectCollaborationRm();
    }

    public override void UpdateBlank(ProjectCollaborationRm entity)
    {
        // NOTHING TO DO HERE
    }

    public IEnumerable<ProjectCollaborationRm> GetAllNonDeletedEntries()
    {
        var all = _projectCollaborationService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<ProjectCollaboration>, List<ProjectCollaborationRm>>(all);
        return returnMap;
    }

    private void FindAndDeleteExistingCollaboration(ProjectCollaboration projectCollaboration)
    {
        var projectCollaborations = _projectCollaborationService.GetAllAsNoTracking().Where(x =>
            x.UserId == projectCollaboration.UserId).ToArray();
        if (!projectCollaborations.Any()) return;
        foreach (var pc in projectCollaborations)
            _projectCollaborationService.DeleteById(pc.Id, true);
    }
}