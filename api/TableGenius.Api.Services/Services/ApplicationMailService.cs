using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Services.Interfaces;
using TableGenius.Api.Settings;
using Microsoft.Extensions.Options;

namespace TableGenius.Api.Services.Services;

public class ApplicationMailService : IApplicationMailService
{
    private readonly GeneralSettings _generalSettings;
    private readonly IApplicationLogger _logger;
    private readonly IMailService _mailService;

    public ApplicationMailService(IMailService mailService, IApplicationLogger logger,
        IOptions<GeneralSettings> generalSettings)
    {
        _mailService = mailService;
        _logger = logger;
        _generalSettings = generalSettings.Value;
    }

    /*public bool InformAboutReservation(Reservation reservation)
    {
        if (string.IsNullOrEmpty(reservation.Mail))
            // Send than no Mail
            return true;

        var subject = string.Format("Reservation für Räume vom " + reservation.Date.ToString("dd.MM.yyyy"));
        var messageForRequester = GetReservationMessage(reservation, true);
        var messageForAktuar = GetReservationMessage(reservation);

        var success = _mailService.SendMail(subject, messageForAktuar, _generalSettings.ReservationMail,
            "Raumreservation",
            FooterType.GENERAL);
        if (success)
            success = _mailService.SendMail(subject, messageForRequester, reservation.Mail, "Raumreservation",
                FooterType.GENERAL);
        return success;
    }

    public bool InformAboutExpense(Expense expense, Attachment attachment)
    {
        var subject = string.Format("Spesenrückerstattung vom " + DateTime.Now.ToString("dd.MM.yyyy"));
        var messageForRequester = GetExpenseMessage(expense, true);
        var messageForFinance = GetExpenseMessage(expense);

        var success = _mailService.SendMail(subject, messageForFinance, _generalSettings.ExpenseMail, "Spesen",
            FooterType.GENERAL, attachment);
        if (success && !string.IsNullOrEmpty(expense.Mail))
            success = _mailService.SendMail(subject, messageForRequester, expense.Mail, "Spesen",
                FooterType.GENERAL, attachment);

        return success;
    }

    private void GetSalutionReservation(StringBuilder message, Reservation reservation, bool requester = false)
    {
        if (requester)
            message.AppendLine("<p><b>Liebe(r) " + reservation.FirstName + "</b></p>");
        else
            message.AppendLine("<p><b>Lieber Aktuar</b></p>");
    }

    private void GetSalutionExpense(StringBuilder message, Expense expense, bool requester = false)
    {
        if (requester)
            message.AppendLine("<p><b>Liebe(r) " + expense.FirstName + "</b></p>");
        else
            message.AppendLine("<p><b>Lieber Finanzer</b></p>");
    }

    private void GetReservation(StringBuilder message, Reservation reservation)
    {
        message.Append("<p>");
        message.Append("<b>Details zur Reservation</b><br/>");
        message.Append("Titel: " + reservation.Title + "<br/>");
        message.Append("Datum: " + reservation.Date.ToString("dd.MM.yyyy") + "<br/>");
        message.Append("Start: " + reservation.Start + "<br/>");
        message.Append("Ende: " + reservation.End + "<br/>");
        message.Append("Resevierende Person: " + reservation.FirstName + " " + reservation.LastName + "<br/>");
        message.Append("Mail: " + reservation.Mail + "<br/>");
        message.Append("Räume: " + reservation.Rooms + "<br/>");
        message.Append("Datum: " + reservation.Date.ToString("dd.MM.yyyy") + "<br/>");
        message.Append("</p>");
    }

    private void GetExpense(StringBuilder message, Expense expense)
    {
        message.Append("<p>");
        message.Append("<b>Personalien</b><br/>");
        message.Append("Vor- und Nachname: " + expense.FirstName + " " + expense.LastName + "<br/>");
        message.Append("Strasse: " + expense.Street + "<br/>");
        message.Append("PLZ und Ort: " + expense.Place + "<br/>");
        message.Append("</p>");
        message.Append("<p>");
        message.Append("<b>Ausgaben</b><br/>");
        message.Append("Arbeitsgebiet: " + expense.Section + "<br/>");
        message.Append("Betrag: " + expense.Amount + "<br/>");
        message.Append("Beschreibung " + expense.Description + "<br/>");
        message.Append("</p>");
    }

    private string GetReservationMessage(Reservation reservation, bool requester = false)
    {
        var sb = new StringBuilder();
        GetSalutionReservation(sb, reservation, requester);
        GetReservation(sb, reservation);
        if (requester)
            sb.AppendLine(
                "Die Reservation wird dir in den nächsten Tag noch bestätigt. Zum jetztigen Zeitpunkt ist diese nur provisorisch und wird online auch nicht angezeigt.");
        sb.AppendLine("<p>Freundliche Grüsse<br/>Die tüchtigen digitalen Wichtel vom Cevi Dürnten</p>");
        return sb.ToString();
    }

    private string GetExpenseMessage(Expense expense, bool requester = false)
    {
        var sb = new StringBuilder();
        GetSalutionExpense(sb, expense, requester);
        GetExpense(sb, expense);
        if (requester)
            sb.AppendLine(
                "Die Spesen liegen nun beim Finanzer zur Bearbeitung. Es wird diese bei Gelegenheit zurückzahlen und dir auf dein Konto vergütten. Bei Fragen wende dich direkt an ihn.");
        sb.AppendLine("<p>Freundliche Grüsse<br/>Die tüchtigen digitalen Wichtel vom Cevi Dürnten</p>");
        return sb.ToString();
    }*/
}