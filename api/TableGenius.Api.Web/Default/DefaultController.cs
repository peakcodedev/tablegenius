using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Default;

[EnableCors("_myAllowSpecificOrigins")]
[Route("[controller]")]
[ApiController]
[Authorize]
public class DefaultController : Controller
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    protected string GetUser()
    {
        var mail = User.Claims.Where(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
            .Select(c => c.Value).SingleOrDefault();
        return mail;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    protected bool IsAdmin()
    {
        var permissions = User.Claims.Where(c => c.Type == "permissions")
            .Select(c => c.Value);
        if (permissions.Contains("admin")) return true;
        return false;
    }
}