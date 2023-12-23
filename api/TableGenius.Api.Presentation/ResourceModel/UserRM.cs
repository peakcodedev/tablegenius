using System;
using TableGenius.Api.Entities.General;

namespace TableGenius.Api.Presentation.ResourceModel;

public class UserRM : BaseRm
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
    public DateTime CreateDate { get; set; }

    public string ApprenticeshipYear { get; set; }
}