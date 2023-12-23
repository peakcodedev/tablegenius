using System.Diagnostics.CodeAnalysis;

namespace TableGenius.Api.Presentation.ResourceModel;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class ExportPrintProjectRM
{
    public int projectid { get; set; }

    public int projectexpert => 0;

    public string projectexpertstart => "";
    public string projectexpertfinish => "";
    public string projectype { get; set; }
    public string projectdescription { get; set; }
    public string projecttitle { get; set; }
    public string projectplacecomment => "";
    public string projectmark => "";
    public string projectpricejury => "";
    public string projectpriceparticipants => "";
    public string projectplacearea => "";
    public string projectplacegridsmall => "";
    public string projectplacegridbig => "";
    public string projectplacegridcorner => "";
    public string projectplaceelectricity230V => "";
    public string projectplaceelectricity400V => "";
    public string projectplaceinternetaccess => "";
    public int projecttnpricefirst => 0;
    public int projecttnpricesecond => 0;
    public int projecttnpricethird => 0;
    public string projectplacelength => "";
    public string projectplacewidth => "";
    public int projectplacex => 0;
    public int projectplacey => 0;
    public int projectyear { get; set; }
    public int projectpublic { get; set; }
}