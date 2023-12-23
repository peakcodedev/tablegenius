using System.Collections.Generic;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Company;

public class Company : Base
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public string City { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Url { get; set; }
    public ICollection<Employee> Employees { get; set; }
}