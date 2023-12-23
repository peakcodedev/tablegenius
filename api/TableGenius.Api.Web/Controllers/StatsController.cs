using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class StatsController : DefaultController
{
    private readonly IStatsPresenter _statsPresenter;

    public StatsController(IStatsPresenter statsPresenter)
    {
        _statsPresenter = statsPresenter;
    }

    [Authorize("admin")]
    public JsonResult GetStats()
    {
        return Json(new SingleDataJsonResult<StatsRm>(200, "stats successfully returned", _statsPresenter.GetStats()));
    }
}