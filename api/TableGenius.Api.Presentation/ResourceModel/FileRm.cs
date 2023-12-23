using Microsoft.AspNetCore.Http;

namespace TableGenius.Api.Presentation.ResourceModel;

public class FileRm : BaseRm
{
    public IFormFile? File { get; set; }
}