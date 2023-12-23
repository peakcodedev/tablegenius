using System;
using Microsoft.AspNetCore.Http;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ProfilePictureRm : BaseRm
{
    public Guid UserId { get; set; }
    public IFormFile File { get; set; }
}