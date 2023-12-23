using System;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Entities.General;
using TableGenius.Api.Entities.Project;

namespace TableGenius.Api.Entities.Admin;

public class User : Base
{
    public Gender Gender { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    public string Profession { get; set; }
    public int Year { get; set; }
    public string Mail { get; set; }
    public bool Confirmed { get; set; }
    public string ProfileImage { get; set; }
    public bool AllowSocialMedia { get; set; }
    public string ApprenticeshipYear { get; set; }
    public virtual ProjectUserRating ProjectUserRating { get; set; }
}