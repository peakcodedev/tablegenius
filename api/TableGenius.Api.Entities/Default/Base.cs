using System;

namespace TableGenius.Api.Entities.Default;

public abstract class Base
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModDate { get; set; }
    public bool Deleted { get; set; }
}