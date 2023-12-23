using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class CourseRM : BaseRm
{
    public int Year { get; set; }
    public string Comments { get; set; }
    public Guid UserId { get; set; }
    public string Answer { get; set; }
}