﻿using System;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Place;

public class LocationAssignment : Base
{
    public virtual Location Location { get; set; }
    public Guid LocationId { get; set; }
    public string Mail { get; set; }
}