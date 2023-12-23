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

public class ProjectBoothService : DatabaseServiceBase<ProjectBooth>, IProjectBoothService
{
    private readonly IFileUploader _fileUploader;

    public ProjectBoothService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger,
        IFileUploader fileUploader) : base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new ProjectBoothQueries(DatabaseUnitOfWork);
        _fileUploader = fileUploader;
    }

    public async Task<string> UploadFile(Guid projectBoothId, MemoryStream fileStream,
        string contentType)
    {
        var projectBooth = GetById(projectBoothId);
        const string folder = "projectBoothLayouts";
        var fileName = projectBooth.Id.ToString("N") + ".pdf";
        var fileUrl = await _fileUploader.UploadFile(folder, fileName, fileStream, contentType);
        projectBooth.ProjectBoothCustomLayout = fileName;
        Update(projectBooth);
        return fileUrl;
    }
}