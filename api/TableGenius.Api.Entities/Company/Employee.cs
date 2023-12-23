using System;
using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Company;

public class Employee : Base
{
    public virtual User User { get; set; }
    public Guid UserId { get; set; }
    public virtual Company Company { get; set; }
    public Guid CompanyId { get; set; }
}