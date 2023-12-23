using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class StatsPresenter : IStatsPresenter
{
    private readonly ICompanyService _companyService;
    private readonly IProfessionService _professionService;
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;

    public StatsPresenter(IProjectService projectService, IUserService userService, ICompanyService companyService,
        IProfessionService professionService)
    {
        _projectService = projectService;
        _userService = userService;
        _companyService = companyService;
        _professionService = professionService;
    }

    public StatsRm GetStats()
    {
        var stats = new StatsRm
        {
            Companies = _companyService.GetAllAsNoTracking().Count,
            Users = _userService.GetAllAsNoTracking().Count,
            Projects = _projectService.GetAllAsNoTracking().Count,
            Professions = _professionService.GetAllAsNoTracking().Count
        };
        return stats;
    }
}