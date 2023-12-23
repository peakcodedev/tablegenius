using System;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Project;

public class ProjectRating : Base
{
    public Guid? ProjectId { get; set; }
    public Guid? ProjectExpertId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double Rating { get; set; }
    public virtual Project Project { get; set; }
    public virtual ProjectExpert ProjectExpert { get; set; }
}