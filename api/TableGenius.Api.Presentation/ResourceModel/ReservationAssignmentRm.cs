using System;
using System.Collections.Generic;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ReservationAssignmentRm : BaseRm
{
    public DateTime BookingDate { get; set; }
    public Guid AreaSlotId { get; set; }
    public Guid ReservationId { get; set; }
    public List<TableReservationAssignmentRm> TableReservationAssignments { get; set; }
}