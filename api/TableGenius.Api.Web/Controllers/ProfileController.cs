using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class ProfileController : DefaultController
{
    private readonly ICoursePresenter _coursePresenter;
    private readonly IUserPresenter _userPresenter;

    public ProfileController(IUserPresenter userPresenter, ICoursePresenter coursePresenter)
    {
        _userPresenter = userPresenter;
        _coursePresenter = coursePresenter;
    }

    [Authorize]
    [HttpGet]
    public JsonResult GetProfile()
    {
        var res = _userPresenter.GetByMail(GetMail());
        return Json(new SingleDataJsonResult<UserRM>(200, "user successfully returned", res));
    }

    [Authorize]
    [HttpPost]
    public JsonResult CreateProfile([FromBody] UserRM user)
    {
        var res = _userPresenter.Add(user);
        return Json(res != null
            ? new SingleDataJsonResult<UserRM>(200, "user successfully created", res)
            : new InfoJsonResult(500, "Error on updating user"));
    }

    [Authorize]
    [HttpPatch]
    public JsonResult UpdateUser(UserRM user)
    {
        var res = _userPresenter.Update(user);
        return Json(res != null
            ? new SingleDataJsonResult<UserRM>(200, "user successfully updated", res)
            : new InfoJsonResult(500, "Error on updating user"));
    }

    [Authorize]
    [HttpPost("course")]
    public JsonResult AddCourse(CourseRM course)
    {
        var res = _coursePresenter.Add(course);
        return Json(res != null
            ? new SingleDataJsonResult<CourseRM>(200, "user course successfully updated", res)
            : new InfoJsonResult(500, "Error on updating user course"));
    }

    [Authorize]
    [HttpGet("course")]
    public JsonResult GetCourse()
    {
        var res = _coursePresenter.GetByUserId(GetUserId(_userPresenter));
        return Json(new SingleDataJsonResult<CourseRM>(200, "user course successfully updated", res));
    }

    [Authorize]
    [HttpPatch("Picture")]
    [DisableRequestSizeLimit]
    public async Task<JsonResult> UploadProfilePicture([FromForm] ProfilePictureRm profilePictureRm)
    {
        var profilePictureMemoryStream = new MemoryStream();
        await profilePictureRm.File.CopyToAsync(profilePictureMemoryStream);
        var res = await _userPresenter.UploadProfilePicture(profilePictureRm.UserId, profilePictureMemoryStream,
            profilePictureRm.File.ContentType);
        return Json(res != null
            ? new SingleDataJsonResult<string>(200, "profile picture successfully updated", res)
            : new InfoJsonResult(500, "Error on updating profile picture"));
    }
}