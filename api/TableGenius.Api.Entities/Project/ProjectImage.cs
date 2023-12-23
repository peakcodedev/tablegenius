using System;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Project;

public class ProjectImage : Base
{
    public Guid ProjectId { get; set; }
    public string Image { get; set; }
    public virtual Project Project { get; set; }
}