using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services.Services;

public class ProjectService : DatabaseServiceBase<Project>, IProjectService
{
    private readonly IFileUploader _fileUploader;
    private readonly IProjectBoothService _projectBoothService;
    private readonly IProjectCollaborationService _projectCollaborationService;

    public ProjectService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger,
        IProjectBoothService projectBoothService,
        IFileUploader fileUploader,
        IProjectCollaborationService projectCollaborationService) : base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new ProjectQueries(DatabaseUnitOfWork);
        _projectBoothService = projectBoothService;
        _projectCollaborationService = projectCollaborationService;
        _fileUploader = fileUploader;
    }

    public Project Add(Project entity, Guid? creatorUserId)
    {
        var res = Add(entity);
        var projectBooth = new ProjectBooth
        {
            ProjectId = res.Id,
            Type = ProjectBoothType.Default
        };
        _projectBoothService.Add(projectBooth);
        if (!creatorUserId.HasValue) return res;
        var projectCollaboration = new ProjectCollaboration
        {
            ProjectId = res.Id,
            UserId = creatorUserId.Value
        };
        _projectCollaborationService.Add(projectCollaboration);
        return res;
    }

    public async Task<string> UploadFile(Guid projectId, MemoryStream fileStream,
        string contentType)
    {
        var project = GetById(projectId);
        const string folder = "projects";
        var fileName = project.Id.ToString("N") + ".pdf";
        var fileUrl = await _fileUploader.UploadFile(folder, fileName, fileStream, contentType);
        project.File = fileName;
        Update(project);
        return fileUrl;
    }

    public Project GetByUserId(Guid userId)
    {
        var projectCollaboration = _projectCollaborationService.GetByUserId(userId);
        return projectCollaboration == null ? null : GetById(projectCollaboration.ProjectId);
    }
}