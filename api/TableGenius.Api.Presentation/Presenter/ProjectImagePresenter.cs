using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class ProjectImagePresenter : BasePresenter<ProjectImageRm, ProjectImage>,
    IProjectImagePresenter
{
    private readonly IMapper _mapper;
    private readonly IProjectImageService _projectImageService;
    private readonly IUserService _userService;

    public ProjectImagePresenter(IMapper mapper, IUserService userService,
        IProjectImageService projectImageService) : base(
        projectImageService, mapper)
    {
        _userService = userService;
        _projectImageService = projectImageService;
        _mapper = mapper;
    }

    public override ProjectImageRm GetBlank()
    {
        return new ProjectImageRm();
    }

    public override void UpdateBlank(ProjectImageRm entity)
    {
        // NOTHING TO DO HERE
    }

    public IEnumerable<ProjectImageRm> GetAllImagesOfProject(Guid projectId)
    {
        var all = _projectImageService.GetAllAsNoTracking().Where(pc => pc.ProjectId == projectId).ToList();

        return (from projectImage in all
            select new ProjectImageRm
            {
                Id = projectImage.Id,
                ProjectId = projectImage.ProjectId,
                Image = projectImage.Image
            }).ToList();
    }

    public async Task<ProjectImageRm> Add(Guid projectId, MemoryStream fileStream, string contentType)
    {
        var res = await _projectImageService.AddProjectImage(projectId, fileStream, contentType);
        var rm = _mapper.Map<ProjectImageRm>(res);
        return rm;
    }
}