using System;
using TableGenius.Api.Entities.Project;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ProjectRm : BaseRm
{
    public string Title { get; set; }
    public string SupervisorName { get; set; }
    public string SupervisorMail { get; set; }
    public string SupervisorPhone { get; set; }
    public bool AllowSocialMedia { get; set; }
    public bool PriceJury { get; set; }
    public int Year { get; set; }
    public ProjectType Type { get; set; }
    public string Description { get; set; }
    public Guid ProjectRatingId { get; set; }
    public Guid ProjectBoothId { get; set; }
    public string VideoUrl { get; set; }
    public string File { get; set; }
    public string TypeString => Type.ToString();
    public DateTime CreateDate { get; set; }
    public string InternalProjectNumber { get; set; }
}