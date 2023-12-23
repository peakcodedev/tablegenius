using System;
using TableGenius.Api.Entities.Project;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ExportProjectBoothRm : BaseRm
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
    public Guid? ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string ProjectBoothTypeString => Type.ToString();
}