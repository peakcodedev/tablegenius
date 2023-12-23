namespace TableGenius.Api.Presentation.ResourceModel;

public class UserRelationsRm
{
    public string ProjectId { get; set; }
    public string CompanyId { get; set; }
    public CompanyRm Company { get; set; }
    public ProjectRm Project { get; set; }
}