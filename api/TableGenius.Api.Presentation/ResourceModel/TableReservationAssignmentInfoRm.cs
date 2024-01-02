using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class TableReservationAssignmentInfoRm : BaseRm
{
    public Guid ReservationAssignmentId { get; set; }
    public TableRm Table { get; set; }
}