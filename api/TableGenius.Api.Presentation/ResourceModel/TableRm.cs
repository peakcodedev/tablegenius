using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class TableRm : BaseRm
{
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public Guid AreaId { get; set; }
}