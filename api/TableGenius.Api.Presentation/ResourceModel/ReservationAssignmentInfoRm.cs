using System;
using System.Collections.Generic;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ReservationAssignmentInfoRm : BaseRm
{
    public DateTime BookingDate { get; set; }
    public AreaSlotRm AreaSlot { get; set; }
    public ReservationRm Reservation { get; set; }
    public List<TableReservationAssignmentInfoRm> TableReservationAssignments { get; set; }
}