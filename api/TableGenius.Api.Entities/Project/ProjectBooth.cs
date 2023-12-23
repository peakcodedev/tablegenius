using System;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Entities.Project;

public class ProjectBooth : Base
{
    public bool Internet { get; set; }
    public bool Electrcity230 { get; set; }
    public bool Electrcity400 { get; set; }
    public ProjectBoothType Type { get; set; }
    public string Comments { get; set; }
    public double Length { get; set; }
    public double Wide { get; set; }
    public long XCoordinate { get; set; }
    public long YCoordinate { get; set; }
    public string ProjectBoothCustomLayout { get; set; }
    public virtual Project Project { get; set; }
    public Guid? ProjectId { get; set; }
}