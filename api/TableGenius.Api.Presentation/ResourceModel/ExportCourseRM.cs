using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ExportCourseRM : BaseRm
{
    public int Year { get; set; }
    public string Comments { get; set; }
    public Guid UserId { get; set; }
    public string Answer { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public int Zip { get; set; }
    public string City { get; set; }
    public string Mail { get; set; }
}