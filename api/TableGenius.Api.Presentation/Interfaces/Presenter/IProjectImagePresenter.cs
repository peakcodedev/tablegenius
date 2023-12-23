using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IProjectImagePresenter : IPresenter<ProjectImageRm>
{
    Task<ProjectImageRm> Add(Guid projectId, MemoryStream fileStream, string contentType);
    IEnumerable<ProjectImageRm> GetAllImagesOfProject(Guid projectId);
}