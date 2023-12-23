using System.Web.Http;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

[AllowAnonymous]
public class HeartbeatController : DefaultController
{
    [Microsoft.AspNetCore.Mvc.HttpGet]
    public JsonResult Current()
    {
        return Json(new InfoJsonResult(200, "System Heartbeat oke"));
    }
}