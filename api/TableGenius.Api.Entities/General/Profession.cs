using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.General;

public class Profession : Base
{
    public string MaleTitle { get; set; }
    public string FemaleTitle { get; set; }
    public int ExternalId { get; set; }
}