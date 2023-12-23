using System;
using System.Runtime.InteropServices.JavaScript;

namespace TableGenius.Api.Presentation.ResourceModel;

public abstract class BaseRm
{
    public Guid Id { get; set; }
    public DateTime ModDate { get; set; }
    public DateTime CreateDate { get; set; }
}