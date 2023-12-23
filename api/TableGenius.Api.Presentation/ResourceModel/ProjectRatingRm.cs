using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ProjectRatingRm : BaseRm
{
    public Guid? ProjectId { get; set; }
    public Guid? ProjectExpertId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double Rating { get; set; }
}