using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class EmployeeModel : BaseRm
{
    public string Mail { get; set; }
    public Guid CompanyId { get; set; }
}