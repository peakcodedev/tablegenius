using System;
using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Project;

public class ProjectUserRating : Base
{
    public Guid UserId { get; set; }
    public string First { get; set; }
    public string Second { get; set; }
    public string Third { get; set; }
    public virtual User User { get; set; }
}