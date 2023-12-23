using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IProjectPresenter : IPresenter<ProjectRm>
{
    ProjectRm Update(ProjectRm entity, bool isAdmin = false);
    ProjectRm Add(ProjectRm entity, Guid? creatorUserId);
    IEnumerable<ProjectRm> GetList();
    IEnumerable<ProjectRm> GetJuryPrices();
    ProjectRm GetProjectByUserId(Guid userId);
    Task<string> UploadFile(Guid projectId, MemoryStream fileStream, string contentType);
}