using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Entities.Project;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IProjectImageService : IDatabaseService<ProjectImage>
{
    Task<ProjectImage> AddProjectImage(Guid projectId, MemoryStream imageStream,
        string contentType);
}