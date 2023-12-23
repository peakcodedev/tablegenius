namespace TableGenius.Api.Settings;

public class MailSettings
{
    public string Host { get; set; }
    public string HostPassword { get; set; }
    public string HostUsername { get; set; }
    public int HostPort { get; set; }
    public string SenderAddress { get; set; }
}