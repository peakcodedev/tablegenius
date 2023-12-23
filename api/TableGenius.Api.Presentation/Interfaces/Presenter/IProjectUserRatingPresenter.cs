using System;
using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IProjectUserRatingPresenter : IPresenter<ProjectUserRatingRM>
{
    ProjectUserRatingRM Add(ProjectUserRatingRM entity);
    ProjectUserRatingRM GetByUserId(Guid userId);
    IEnumerable<ExportProjectUserRatingRM> GetExport();
}