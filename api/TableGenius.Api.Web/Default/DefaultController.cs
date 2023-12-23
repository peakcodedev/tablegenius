using System;
using System.Linq;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Default;

[EnableCors("_myAllowSpecificOrigins")]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DefaultController : Controller
{
    protected string GetMail()
    {
        var mail = User.Claims.Where(c => c.Type == "http://TableGenius.ch/email")
            .Select(c => c.Value).SingleOrDefault();
        return mail;
    }

    protected Guid GetUserId(IUserPresenter userPresenter)
    {
        var mail = GetMail();
        var user = userPresenter.GetByMail(mail);
        return user?.Id ?? Guid.Empty;
    }

    protected bool isAdmin()
    {
        var permissions = User.Claims.Where(c => c.Type == "permissions")
            .Select(c => c.Value);
        if (permissions.Contains("admin")) return true;
        return false;
    }
}