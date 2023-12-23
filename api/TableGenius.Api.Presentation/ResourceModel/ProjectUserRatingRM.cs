using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ProjectUserRatingRM : BaseRm
{
    public string First { get; set; }
    public string Second { get; set; }
    public string Third { get; set; }
    public Guid UserId { get; set; }
}