using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Entities.Project;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IProjectService : IDatabaseService<Project>
{
    Project Add(Project entity, Guid? creatorUserId);

    Task<string> UploadFile(Guid projectId, MemoryStream fileStream,
        string contentType);

    Project GetByUserId(Guid userId);
}