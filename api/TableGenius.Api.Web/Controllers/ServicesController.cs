using TableGenius.Api.Settings;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace TableGenius.Api.Web.Controllers;

[AllowAnonymous]
public class ServicesController : DefaultController
{
    private readonly GeneralSettings _generalSettings;

    public ServicesController(IOptions<GeneralSettings> settings)
    {
        _generalSettings = settings.Value;
    }
}