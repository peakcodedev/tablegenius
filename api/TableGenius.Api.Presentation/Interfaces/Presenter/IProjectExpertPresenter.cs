using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IProjectExpertPresenter : IPresenter<ProjectExpertRm>
{
    ProjectExpertRm Update(ProjectExpertRm entity);
    ProjectExpertRm Add(ProjectExpertRm entity);
    IEnumerable<ProjectExpertRm> GetList();
}