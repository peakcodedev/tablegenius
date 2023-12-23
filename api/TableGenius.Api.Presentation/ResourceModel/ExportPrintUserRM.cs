using System.Diagnostics.CodeAnalysis;

namespace TableGenius.Api.Presentation.ResourceModel;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class ExportPrintUserRM
{
    public int userid { get; set; }
    public int userproject { get; set; }
    public string usergroup => "";
    public string usergender => "";
    public string username => "";
    public string userfirstname { get; set; }
    public string userlastname { get; set; }
    public string usermobilephone => "";
    public string userfixedphone => "";
    public string useremail => "";
    public string userpassword => "";
    public string useraddress => "";
    public string userplz => "";
    public string usercity => "";
    public string userprofession { get; set; }
    public string userdateofbirth => "";
    public string userimage { get; set; }
    public string usereducationalyear { get; set; }
    public string usercontactpersonfirstname => "";
    public string usercontactpersonlastname => "";
    public string usercontactpersonfixedphone => "";
    public string usercontactpersonmobilephone => "";
    public string usercontactpersonemail => "";
    public string usercompanyname { get; set; }
    public string usercompanyaddress => "";
    public string usercompanycity => "";
    public string usercompanyplz => "";
    public string usercompanydescription { get; set; }
    public string usercompanyimage { get; set; }
    public string userschooldayone => "";
    public string userschooldaytwo => "";
    public string userpersence => "";
    public string usercourse => "";
    public string usernotebythesecretariat => "";
    public int userstatus => 0;
    public string userbrunch => "Nein";
    public string usercompanywebsite => "";
}