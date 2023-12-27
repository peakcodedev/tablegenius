using System;
using System.Collections.Generic;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Entities.Reservations;

namespace TableGenius.Api.Entities.Place;

public class Table : TenantBase
{
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public virtual Area Area { get; set; }
    public Guid AreaId { get; set; }
    public ICollection<TableReservationAssignment> TableReservationAssignments { get; set; }
}