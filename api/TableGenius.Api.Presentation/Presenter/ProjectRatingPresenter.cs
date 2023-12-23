using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class ProjectRatingPresenter : BasePresenter<ProjectRatingRm, ProjectRating>, IProjectRatingPresenter
{
    private readonly IMapper _mapper;
    private readonly IProjectRatingService _projectRatingService;

    public ProjectRatingPresenter(IMapper mapper, IProjectRatingService projectRatingService) : base(
        projectRatingService, mapper)
    {
        _projectRatingService = projectRatingService;
        _mapper = mapper;
    }

    public ProjectRatingRm Add(ProjectRatingRm entity)
    {
        var model = _mapper.Map<ProjectRating>(entity);
        var result = _projectRatingService.Add(model);
        return _mapper.Map<ProjectRating, ProjectRatingRm>(result);
    }

    public override ProjectRatingRm GetBlank()
    {
        return new ProjectRatingRm();
    }

    public ProjectRatingRm Update(ProjectRatingRm entity)
    {
        var db = _mapper.Map<ProjectRatingRm, ProjectRating>(entity);
        var elem = _projectRatingService.Update(db);
        return _mapper.Map<ProjectRating, ProjectRatingRm>(elem);
    }

    public override void UpdateBlank(ProjectRatingRm entity)
    {
        // NOTHING TO DO HERE
    }

    public List<ProjectRatingRm> GetList()
    {
        var all = _projectRatingService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<ProjectRating>, List<ProjectRatingRm>>(all);
        return returnMap;
    }

    public ProjectRatingRm GetProjectRatingByProjectId(Guid projectId)
    {
        var all = _projectRatingService.GetAllAsNoTracking().ToArray();
        var projectRating = _projectRatingService.GetAllAsNoTracking()
            .SingleOrDefault(c => projectId == c.ProjectId);
        if (projectRating == null) return null;
        var projectRatingRm = _mapper.Map<ProjectRating, ProjectRatingRm>(projectRating);
        return projectRatingRm;
    }
}