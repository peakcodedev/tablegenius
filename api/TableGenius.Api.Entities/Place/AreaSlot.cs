using System;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Place;

public class AreaSlot : TenantBase
{
    public virtual Area Area { get; set; }
    public Guid AreaId { get; set; }
    public int? Length { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public AreaSlotType Type { get; set; }
    public string Name { get; set; }
}