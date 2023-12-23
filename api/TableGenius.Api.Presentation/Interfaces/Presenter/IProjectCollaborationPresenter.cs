using System;
using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IProjectCollaborationPresenter : IPresenter<ProjectCollaborationRm>
{
    ProjectCollaborationRm Add(ProjectCollaborationModel entity);
    IEnumerable<ProjectCollaborationRm> GetList(Guid projectId);
    IEnumerable<ProjectCollaborationRm> GetAllNonDeletedEntries();
}