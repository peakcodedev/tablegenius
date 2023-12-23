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

public class ProjectImageService : DatabaseServiceBase<ProjectImage>, IProjectImageService
{
    private readonly IFileUploader _fileUploader;

    public ProjectImageService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IFileUploader fileUploader,
        IApplicationLogger logger) :
        base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new ProjectImageQueries(DatabaseUnitOfWork);
        _fileUploader = fileUploader;
    }

    public async Task<ProjectImage> AddProjectImage(Guid projectId, MemoryStream imageStream,
        string contentType)
    {
        var projectImage = new ProjectImage
        {
            ProjectId = projectId
        };
        var createdProjectImage = Add(projectImage);
        const string folder = "projectImages";
        var fileName = createdProjectImage.Id.ToString("N");
        var imageUrl = await _fileUploader.UploadFile(folder, fileName, imageStream, contentType);
        createdProjectImage.Image = fileName;
        var res = Update(createdProjectImage);
        return res;
    }
}