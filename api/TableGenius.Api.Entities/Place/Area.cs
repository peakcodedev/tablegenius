﻿using System.Collections.Generic;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Place;

public class Area : TenantBase
{
    public string Name { get; set; }
    public string BlueprintUrl { get; set; }
    public virtual ICollection<Table> Tables { get; set; }
    public virtual ICollection<AreaSlot> AreaSlots { get; set; }
}