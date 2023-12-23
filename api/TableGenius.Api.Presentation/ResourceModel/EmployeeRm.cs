using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class EmployeeRm : BaseRm
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mail { get; set; }
}