using System;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Entities.Reservations;

public class TableReservationAssignment
{
    public Guid ReservationAssignmentId { get; set; }
    public ReservationAssignment ReservationAssignment { get; set; }
    public Guid TableId { get; set; }
    public Table Table { get; set; }
    public Guid TenantId { get; set; }
}