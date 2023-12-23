using System;
using TableGenius.Api.Entities.General;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ProjectExpertRm : BaseRm
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
}