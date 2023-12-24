using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Place;

public class Table : TenantBase
{
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
}