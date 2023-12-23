using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

[Authorize("admin")]
public class UsersController : DefaultController
{
    private readonly IUserPresenter _userPresenter;

    public UsersController(IUserPresenter userPresenter)
    {
        _userPresenter = userPresenter;
    }


    [HttpGet]
    public JsonResult GetList()
    {
        var res = _userPresenter.GetList();
        return Json(new DataJsonResult<UserRM>(200, "users successfully returned", res));
    }


    [HttpPost]
    public JsonResult Add(UserRM user)
    {
        var res = _userPresenter.Add(user);
        return Json(res != null
            ? new SingleDataJsonResult<UserRM>(200, "successfully added user", res)
            : new InfoJsonResult(500, "Error on adding user"));
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _userPresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted user")
            : new InfoJsonResult(500, "Error on deleting user"));
    }

    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] UserRM userRm)
    {
        userRm.Id = id;
        var res = _userPresenter.Update(userRm);
        return Json(res != null
            ? new SingleDataJsonResult<UserRM>(200, "user successfully updated", res)
            : new InfoJsonResult(500, "Error on updating user"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _userPresenter.GetById(id);
        return Json(new SingleDataJsonResult<UserRM>(200, "user successfully returned", res));
    }

    [HttpGet("{id}/relations")]
    public JsonResult GetRelations(Guid id)
    {
        var res = _userPresenter.GetUserRelations(id);
        return Json(new SingleDataJsonResult<UserRelationsRm>(200, "user relations successfully returned", res));
    }

    [HttpPatch("{id}/Picture")]
    [DisableRequestSizeLimit]
    public async Task<JsonResult> UploadUserPicture([FromForm] ProfilePictureRm profilePictureRm)
    {
        var pictureMemoryStream = new MemoryStream();
        await profilePictureRm.File.CopyToAsync(pictureMemoryStream);
        var res = await _userPresenter.UploadProfilePicture(profilePictureRm.UserId, pictureMemoryStream,
            profilePictureRm.File.ContentType);
        return Json(res != null
            ? new SingleDataJsonResult<string>(200, "profile picture successfully updated", res)
            : new InfoJsonResult(500, "Error on updating profile picture"));
    }
}