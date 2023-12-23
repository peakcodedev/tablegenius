using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Place;

public class Location : Base
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
}