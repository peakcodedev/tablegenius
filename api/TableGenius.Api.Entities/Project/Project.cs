using System;
using System.Collections.Generic;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Project;

public class Project : Base
{
    public string Title { get; set; }
    public string SupervisorName { get; set; }
    public string SupervisorPhone { get; set; }
    public string SupervisorMail { get; set; }
    public bool AllowSocialMedia { get; set; }
    public bool PriceJury { get; set; }
    public int Year { get; set; }
    public ProjectType Type { get; set; }
    public string Description { get; set; }
    public Guid ProjectRatingId { get; set; }
    public Guid ProjectBoothId { get; set; }
    public string VideoUrl { get; set; }
    public string InternalProjectNumber { get; set; }
    public string File { get; set; }
    public virtual ProjectRating ProjectRating { get; set; }
    public virtual ProjectBooth ProjectBooth { get; set; }
    public ICollection<ProjectCollaboration> ProjectCollaborations { get; set; }
    public ICollection<ProjectImage> ProjectImages { get; set; }
}