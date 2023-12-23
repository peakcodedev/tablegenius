using System;
using System.Collections.Generic;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Entities.General;

namespace TableGenius.Api.Entities.Project;

public class ProjectExpert : Base
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public virtual ICollection<ProjectRating> ProjectRatings { get; set; }
}