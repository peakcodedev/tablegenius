using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Services.Interfaces;
using TableGenius.Api.Settings;
using Microsoft.Extensions.Options;

namespace TableGenius.Api.Services.Services;

public class MailService : IMailService
{
    private readonly GeneralSettings _generalSettings;
    private readonly IApplicationLogger _logger;
    private readonly MailSettings _settings;

    public MailService(IOptions<MailSettings> settings, IApplicationLogger logger,
        IOptions<GeneralSettings> generalSettings)
    {
        _settings = settings.Value;
        _logger = logger;
        _generalSettings = generalSettings.Value;
    }

    public bool SendMail(string subject, string messageBody, string recipient, string header, FooterType type, Attachment attachment = null)
    {
        var success = false;
        var message = new MailMessage(_settings.SenderAddress, recipient)
        {
            Subject = _generalSettings.GroupName + " | " + subject,
            SubjectEncoding = Encoding.UTF8,
            BodyEncoding = Encoding.Unicode,
            IsBodyHtml = true
        };
        BuildMailMessage(message, messageBody, header, type);
        if (attachment != null)
        {
            message.Attachments.Add(attachment);
        }
        var client = new SmtpClient(_settings.Host, _settings.HostPort)
        {
            EnableSsl = true,
            Timeout = 500000000,
            Credentials = new NetworkCredential(_settings.HostUsername, _settings.HostPassword)
        };
        try
        {
            client.Send(message);
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            _logger.LogError(ex, "Error on sending mail");
        }

        return success;
    }

    public bool SendMailInNameof(string subject, string messageBody, string recipient, string sender, string header,
        FooterType type)
    {
        var success = true;
        var message = new MailMessage(sender, recipient)
        {
            Subject = _generalSettings.GroupName + " | " + subject,
            SubjectEncoding = Encoding.UTF8,
            BodyEncoding = Encoding.UTF8,
            IsBodyHtml = true
        };
        BuildMailMessage(message, messageBody, header, type);
        var client = new SmtpClient(_settings.Host, _settings.HostPort)
        {
            EnableSsl = true,
            Timeout = 500000000,
            Credentials = new NetworkCredential(_settings.HostUsername, _settings.HostPassword)
        };
        try
        {
            client.Send(message);
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            _logger.LogError(ex, "Error on sending mail");
        }

        return success;
    }

    private void BuildMailMessage(MailMessage message, string messageBody, string header, FooterType type)
    {
        Stream htmlStream = null;
        Stream logoStream = null;

        message.Headers.Add("Content-Type", "content=text/html; charset=\"UTF-8\"");

        try
        {
            var resourceAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == "TableGenius.Api.Settings");

            htmlStream = resourceAssembly.GetManifestResourceStream("TableGenius.Api.Settings.Templates.MailTemplate.html");
            var reader = new StreamReader(htmlStream);
            var body = reader.ReadToEnd().Replace("%MAIN_CONTENT%", messageBody);
            body = body.Replace("%HEADER%", header.ToUpper());
            body = body.Replace("%FOOTER%", GetFooter(type));
            body = body.Replace("%GROUP%", _generalSettings.GroupName.ToUpper());

            var altView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Html);
            message.AlternateViews.Add(altView);
            message.IsBodyHtml = true;

            logoStream = resourceAssembly.GetManifestResourceStream("TableGenius.Api.Settings.Images.Logo.png");
            var logo = new LinkedResource(logoStream, "image/png")
            {
                ContentId = "Logo.png"
            };
            altView.LinkedResources.Add(logo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on building mail");
        }
    }

    private string GetFooter(FooterType type)
    {
        switch (type)
        {
            case FooterType.GENERAL:
                return "DIESES MAIL WURDE AUTOMATISCH VERSCHICKT.";
            case FooterType.APPOINTMENT:
                return
                    "DIESES MAIL WURDE AUTOMATISCH VERSCHICKT. FRAGEN RICHTEN SIE BITTE AN DIE ENTSPRECHENDEN GRUPPENLEITER ODER AN DIE ABTEILUNGSLEITUNG UNTER AL@CEVIDUERNTEN.CH";
            default:
                return "";
        }
    }
}