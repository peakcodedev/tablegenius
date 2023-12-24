using System;

namespace TableGenius.Api.Entities.Default;

public abstract class TenantBase : Base
{
    public Guid TenantId { get; set; }
}