using System;

namespace TableGenius.Api.Presentation.ResourceModel;

public class ExportProjectUserRatingRM
{
    public string InternalProjectNumber { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Count { get; set; }
    public string TypeString { get; set; }
}