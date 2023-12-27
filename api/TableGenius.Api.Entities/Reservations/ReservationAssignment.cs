using System;
using System.Collections.Generic;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Entities.Reservations;

public class ReservationAssignment : TenantBase
{
    public DateTime BookingDate { get; set; }
    public Guid ReservationId { get; set; }
    public Reservation Reservation { get; set; }
    public Guid AreaSlotId { get; set; }
    public AreaSlot AreaSlot { get; set; }
    public ICollection<TableReservationAssignment> TableReservationAssignments { get; set; }
}