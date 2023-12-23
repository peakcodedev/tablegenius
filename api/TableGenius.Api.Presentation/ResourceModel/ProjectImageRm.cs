using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ProjectImageRm : BaseRm
{
    public Guid ProjectId { get; set; }
    public string Image { get; set; }
}