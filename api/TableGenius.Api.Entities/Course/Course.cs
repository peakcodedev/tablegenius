using System;
using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Course;

public class Course : Base
{
    public int Year { get; set; }
    public string Comments { get; set; }
    public string Answer { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}