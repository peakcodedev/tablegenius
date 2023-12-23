namespace TableGenius.Api.Presentation.ResourceModel;

public class CompanyRm : BaseRm
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public string City { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Url { get; set; }
}