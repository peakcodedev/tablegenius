using System;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Reservations;

public class Reservation : TenantBase
{
    public string Name { get; set; }
    public DateTime BookingDate { get; set; }
    public string Comments { set; get; }
    public int Count { get; set; }
    public string PhoneNumber { get; set; }
    public virtual ReservationAssignment ReservationAssignment { get; set; }
}