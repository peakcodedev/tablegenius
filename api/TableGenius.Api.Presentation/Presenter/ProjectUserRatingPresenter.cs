using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class ProjectUserRatingPresenter : BasePresenter<ProjectUserRatingRM, ProjectUserRating>,
    IProjectUserRatingPresenter
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;
    private readonly IProjectUserRatingService _projectUserRatingService;

    public ProjectUserRatingPresenter(IMapper mapper,
        IProjectUserRatingService projectUserRatingService, IProjectService projectService) : base(
        projectUserRatingService, mapper)
    {
        _projectUserRatingService = projectUserRatingService;
        _mapper = mapper;
        _projectService = projectService;
    }

    public ProjectUserRatingRM GetByUserId(Guid userId)
    {
        var result = _projectUserRatingService.GetByUserId(userId);
        return _mapper.Map<ProjectUserRatingRM>(result);
    }

    public IEnumerable<ExportProjectUserRatingRM> GetExport()
    {
        var list = new List<ExportProjectUserRatingRM>();
        var allRatings = _projectUserRatingService.GetAllAsNoTracking();
        foreach (var project in _projectService.GetAllAsNoTracking())
        {
            var first = allRatings.Count(x => project.Id == Guid.Parse(x.First));
            var second = allRatings.Count(x => project.Id == Guid.Parse(x.Second));
            var third = allRatings.Count(x => project.Id == Guid.Parse(x.Third));
            var total = first + second + third;
            list.Add(new ExportProjectUserRatingRM
            {
                Id = project.Id, InternalProjectNumber = project.InternalProjectNumber, Count = total,
                Title = project.Title, TypeString = project.Type.ToString()
            });
        }

        return list;
    }

    public override ProjectUserRatingRM GetBlank()
    {
        return new ProjectUserRatingRM();
    }

    public override void UpdateBlank(ProjectUserRatingRM entity)
    {
        // NOTHING TO DO HERE
    }

    public ProjectUserRatingRM Add(ProjectUserRatingRM entity)
    {
        var model = _mapper.Map<ProjectUserRating>(entity);
        var result = _projectUserRatingService.Add(model);
        return _mapper.Map<ProjectUserRatingRM>(result);
    }
}