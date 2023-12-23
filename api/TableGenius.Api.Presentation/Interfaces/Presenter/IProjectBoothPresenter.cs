using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IProjectBoothPresenter : IPresenter<ProjectBoothRm>
{
    ProjectBoothRm Update(ProjectBoothRm entity);
    bool Add(ProjectBoothRm entity);
    List<ProjectBoothRm> GetList();
    List<ExportProjectBoothRm> GetExportList();
    ProjectBoothRm GetProjectBoothByUserId(Guid userId);
    ProjectBoothRm GetProjectBoothByProjectId(Guid projectId);
    Task<string> UploadFile(Guid projectBoothId, MemoryStream fileStream, string contentType);
}