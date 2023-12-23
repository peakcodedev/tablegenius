namespace TableGenius.Api.Presentation.ResourceModel;

public class ExportPriceRM : BaseRm
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProjectName { get; set; }
    public int Year { get; set; }
    public string CompanyName { get; set; }
    public string Profession { get; set; }
    public string ApprenticeshipYear { get; set; }
    public string ProjectType { get; set; }
    public int ProjectCount { get; set; }
    public int PointsUserPrice { get; set; }
}