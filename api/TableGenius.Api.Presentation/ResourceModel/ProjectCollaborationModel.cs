using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ProjectCollaborationModel : BaseRm
{
    public string Mail { get; set; }
    public Guid ProjectId { get; set; }
}