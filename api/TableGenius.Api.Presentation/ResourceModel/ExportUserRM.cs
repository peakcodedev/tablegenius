namespace TableGenius.Api.Presentation.ResourceModel;

public class ExportUserRM : UserRM
{
    public string ProjectId { get; set; }
    public string CompanyId { get; set; }
    public string GenderString => Gender.ToString();
}