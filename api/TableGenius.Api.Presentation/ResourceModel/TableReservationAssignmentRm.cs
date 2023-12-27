using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class TableReservationAssignmentRm : BaseRm
{
    public Guid ReservationAssignmentId { get; set; }
    public Guid TableId { get; set; }
}