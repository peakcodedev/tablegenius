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

public class ProjectBoothPresenter : BasePresenter<ProjectBoothRm, ProjectBooth>, IProjectBoothPresenter
{
    private readonly IMapper _mapper;
    private readonly IProjectBoothService _projectBoothService;
    private readonly IProjectCollaborationService _projectCollaborationService;
    private readonly IProjectService _projectService;

    public ProjectBoothPresenter(IMapper mapper, IProjectBoothService projectBoothService,
        IProjectCollaborationService projectCollaborationService, IProjectService projectService) : base(
        projectBoothService, mapper)
    {
        _projectBoothService = projectBoothService;
        _mapper = mapper;
        _projectCollaborationService = projectCollaborationService;
        _projectService = projectService;
    }

    public bool Add(ProjectBoothRm entity)
    {
        var model = _mapper.Map<ProjectBooth>(entity);
        var result = _projectBoothService.Add(model);
        var success = result != null;
        return success;
    }

    public override ProjectBoothRm GetBlank()
    {
        return new ProjectBoothRm();
    }

    public override void UpdateBlank(ProjectBoothRm entity)
    {
        // NOTHING TO DO HERE
    }

    public List<ProjectBoothRm> GetList()
    {
        var all = _projectBoothService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<ProjectBooth>, List<ProjectBoothRm>>(all);
        return returnMap;
    }

    public List<ExportProjectBoothRm> GetExportList()
    {
        var all = _projectBoothService.GetAllAsNoTracking().ToList();
        var exportProjectBooths = _mapper.Map<IEnumerable<ProjectBooth>, List<ExportProjectBoothRm>>(all);
        foreach (var projectBooth in exportProjectBooths)
        {
            if (!projectBooth.ProjectId.HasValue) continue;
            var project = _projectService.GetByIdAsNoTracking(projectBooth.ProjectId.Value);
            if (project == null) continue;
            projectBooth.ProjectName = project.Title;
        }

        return exportProjectBooths;
    }

    public ProjectBoothRm GetProjectBoothByUserId(Guid userId)
    {
        var projectCollaboration =
            _projectCollaborationService.GetAllAsNoTracking().SingleOrDefault(e => e.UserId == userId);
        if (projectCollaboration == null) return null;
        var projectBooth = _projectBoothService.GetAllAsNoTracking()
            .SingleOrDefault(c => projectCollaboration.ProjectId == c.ProjectId);
        var projectBoothRm = _mapper.Map<ProjectBooth, ProjectBoothRm>(projectBooth);
        return projectBoothRm;
    }

    public ProjectBoothRm Update(ProjectBoothRm entity)
    {
        var existingData = _projectBoothService.GetByIdAsNoTracking(entity.Id);
        entity.ProjectBoothCustomLayout = existingData.ProjectBoothCustomLayout;
        if (entity.Type != ProjectBoothType.SpecialCase) entity.ProjectBoothCustomLayout = "";
        var db = _mapper.Map<ProjectBoothRm, ProjectBooth>(entity);
        var elem = _projectBoothService.Update(db);
        return _mapper.Map<ProjectBooth, ProjectBoothRm>(elem);
    }

    public ProjectBoothRm GetProjectBoothByProjectId(Guid projectId)
    {
        var projectBooth = _projectBoothService.GetAllAsNoTracking()
            .Single(c => projectId == c.ProjectId);
        var projectBoothRm = _mapper.Map<ProjectBooth, ProjectBoothRm>(projectBooth);
        return projectBoothRm;
    }

    public Task<string> UploadFile(Guid projectBoothId, MemoryStream fileStream, string contentType)
    {
        return _projectBoothService.UploadFile(projectBoothId, fileStream, contentType);
    }
}