using System;
using Microsoft.AspNetCore.Http;

namespace TableGenius.Api.Presentation.ResourceModel;

public class CompanyPictureRm : BaseRm
{
    public Guid CompanyId { get; set; }
    public IFormFile File { get; set; }
}