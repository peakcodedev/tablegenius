using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class ProjectExpertPresenter : BasePresenter<ProjectExpertRm, ProjectExpert>, IProjectExpertPresenter
{
    private readonly IMapper _mapper;
    private readonly IProjectExpertService _projectExpertService;

    public ProjectExpertPresenter(IMapper mapper, IProjectExpertService projectExpertService) : base(
        projectExpertService, mapper)
    {
        _projectExpertService = projectExpertService;
        _mapper = mapper;
    }

    public override ProjectExpertRm GetBlank()
    {
        return new ProjectExpertRm();
    }

    public override void UpdateBlank(ProjectExpertRm entity)
    {
        // NOTHING TO DO HERE
    }

    public IEnumerable<ProjectExpertRm> GetList()
    {
        var all = _projectExpertService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<ProjectExpert>, List<ProjectExpertRm>>(all);
        return returnMap;
    }

    public ProjectExpertRm Update(ProjectExpertRm entity)
    {
        var db = _mapper.Map<ProjectExpertRm, ProjectExpert>(entity);
        var elem = _projectExpertService.Update(db);
        return _mapper.Map<ProjectExpert, ProjectExpertRm>(elem);
    }

    public ProjectExpertRm Add(ProjectExpertRm entity)
    {
        var model = _mapper.Map<ProjectExpert>(entity);
        var result = _projectExpertService.Add(model);
        return _mapper.Map<ProjectExpertRm>(result);
    }
}