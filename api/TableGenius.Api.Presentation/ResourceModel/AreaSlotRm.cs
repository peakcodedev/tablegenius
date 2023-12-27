using System;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Presentation.ResourceModel;

public class AreaSlotRm : BaseRm
{
    public AreaSlotType Type { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public int? Length { get; set; }
    public Guid AreaId { get; set; }
    public string Name { get; set; }

    public string TypeString
    {
        get
        {
            switch (Type)
            {
                case AreaSlotType.TimeSlotLength:
                    return "Zeitslot (Länge in Stunden)";
                case AreaSlotType.OnePerDay:
                    return "Einmal pro Tag";
                case AreaSlotType.StartAndEnd:
                    return "Zeitslot (von/bis)";
                default:
                    return "";
            }
        }
    }
}