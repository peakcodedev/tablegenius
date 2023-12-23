using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ReservationRm : BaseRm
{
    public string Name { get; set; }
    public DateTime BookingDate { get; set; }
    public string Comments { set; get; }
    public int Count { get; set; }
    public string PhoneNumber { get; set; }
}