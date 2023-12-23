using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ProjectCollaborationRm : BaseRm
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mail { get; set; }
}