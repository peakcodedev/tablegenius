using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Place;

public class Area : TenantBase
{
    public string Name { get; set; }
    public string BlueprintUrl { get; set; }
}