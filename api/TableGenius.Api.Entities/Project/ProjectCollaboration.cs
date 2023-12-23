using System;
using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Project;

public class ProjectCollaboration : Base
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public virtual Project Project { get; set; }
}