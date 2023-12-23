using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Entities.Project;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IProjectBoothService : IDatabaseService<ProjectBooth>
{
    Task<string> UploadFile(Guid projectBoothId, MemoryStream fileStream,
        string contentType);
}