using System;
using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IProjectRatingPresenter : IPresenter<ProjectRatingRm>
{
    ProjectRatingRm Update(ProjectRatingRm entity);
    ProjectRatingRm Add(ProjectRatingRm entity);
    List<ProjectRatingRm> GetList();
    ProjectRatingRm GetProjectRatingByProjectId(Guid projectId);
}