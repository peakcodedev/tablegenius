using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class ProjectPresenter : BasePresenter<ProjectRm, Project>, IProjectPresenter
{
    private readonly IMapper _mapper;
    private readonly IProjectCollaborationService _projectCollaborationService;
    private readonly IProjectService _projectService;

    public ProjectPresenter(IMapper mapper, IProjectService projectService,
        IProjectCollaborationService projectCollaborationService) : base(
        projectService, mapper)
    {
        _projectService = projectService;
        _projectCollaborationService = projectCollaborationService;
        _mapper = mapper;
    }

    public override ProjectRm GetBlank()
    {
        return new ProjectRm();
    }

    public override void UpdateBlank(ProjectRm entity)
    {
        // NOTHING TO DO HERE
    }

    public IEnumerable<ProjectRm> GetList()
    {
        var all = _projectService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<Project>, List<ProjectRm>>(all);
        return returnMap;
    }

    public ProjectRm GetProjectByUserId(Guid userId)
    {
        var projectCollaboration =
            _projectCollaborationService.GetAllAsNoTracking().SingleOrDefault(e => e.UserId == userId);
        if (projectCollaboration == null) return null;
        var project = _projectService.GetAllAsNoTracking().Single(c => projectCollaboration.ProjectId == c.Id);
        var projectRm = _mapper.Map<Project, ProjectRm>(project);
        return projectRm;
    }

    public ProjectRm Add(ProjectRm entity, Guid? creatorUserId)
    {
        var model = _mapper.Map<Project>(entity);
        var result = _projectService.Add(model, creatorUserId);
        return _mapper.Map<ProjectRm>(result);
    }

    public Task<string> UploadFile(Guid projectId, MemoryStream fileStream, string contentType)
    {
        return _projectService.UploadFile(projectId, fileStream, contentType);
    }

    public ProjectRm Update(ProjectRm entity, bool isAdmin = false)
    {
        var existingData = _projectService.GetByIdAsNoTracking(entity.Id);
        entity.File = existingData.File;
        if (!isAdmin) entity.PriceJury = existingData.PriceJury;
        var db = _mapper.Map<ProjectRm, Project>(entity);
        var elem = _projectService.Update(db);
        return _mapper.Map<Project, ProjectRm>(elem);
    }

    public IEnumerable<ProjectRm> GetJuryPrices()
    {
        var all = _projectService.GetAllAsNoTracking().Where(x => x.PriceJury).ToList();
        var returnMap = _mapper.Map<IEnumerable<Project>, List<ProjectRm>>(all);
        return returnMap;
    }
}