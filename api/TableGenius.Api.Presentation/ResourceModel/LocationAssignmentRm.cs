using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class LocationAssignmentRm : BaseRm
{
    public string Mail { get; set; }
    public Guid LocationId { get; set; }
}