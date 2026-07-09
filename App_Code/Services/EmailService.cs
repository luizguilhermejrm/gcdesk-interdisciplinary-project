using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

/// <summary>Static service for sending email notifications via SMTP.</summary>
public static class EmailService
{
    /// <summary>SMTP host from Web.config (smtpHost).</summary>
    private static string SmtpHost => ConfigurationManager.AppSettings["smtpHost"] ?? "localhost";
    /// <summary>SMTP port from Web.config (smtpPort).</summary>
    private static int SmtpPort => int.TryParse(ConfigurationManager.AppSettings["smtpPort"], out int p) ? p : 25;
    /// <summary>SMTP username from Web.config (smtpUser).</summary>
    private static string SmtpUser => ConfigurationManager.AppSettings["smtpUser"] ?? "";
    /// <summary>SMTP password from Web.config (smtpPass).</summary>
    private static string SmtpPass => ConfigurationManager.AppSettings["smtpPass"] ?? "";

    /// <summary>Sends an HTML email to the specified recipient.</summary>
    /// <param name="to">Recipient email address.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="body">HTML email body.</param>
    /// <returns>True if sent successfully, false otherwise.</returns>
    public static bool Send(string to, string subject, string body)
    {
        try
        {
            using (SmtpClient client = new SmtpClient(SmtpHost, SmtpPort))
            {
                client.EnableSsl = SmtpPort == 587;
                if (!string.IsNullOrEmpty(SmtpUser))
                    client.Credentials = new NetworkCredential(SmtpUser, SmtpPass);

                using (MailMessage msg = new MailMessage("noreply@gcdesk.com", to, subject, body))
                {
                    msg.IsBodyHtml = true;
                    client.Send(msg);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>Notifies the requester that their ticket was created.</summary>
    public static void NotifyNewTicket(Ticket ticket, User user)
    {
        Send(user.Email, "Chamado Criado - GC Desk",
            $"<p>Olá {user.Name},</p><p>Seu chamado #{ticket.TicketId} foi criado com sucesso.</p>");
    }

    /// <summary>Notifies the requester that their ticket was accepted by an analyst.</summary>
    public static void NotifyTicketAccepted(Ticket ticket, User user)
    {
        Send(user.Email, "Chamado em Andamento - GC Desk",
            $"<p>Olá {user.Name},</p><p>Seu chamado #{ticket.TicketId} foi aceito por um analista.</p>");
    }

    /// <summary>Notifies the requester that their ticket has been finished.</summary>
    public static void NotifyTicketFinished(Ticket ticket, User user)
    {
        string surveyLink = $"http://localhost:8080/Pages/Sistema/Colaborador/AvaliarChamado/Default.aspx?id={ticket.TicketId}";
        Send(user.Email, "Chamado Finalizado - GC Desk",
            $"<p>Olá {user.Name},</p><p>Seu chamado #{ticket.TicketId} foi finalizado.</p>"
            + $"<p>Avalie o atendimento: <a href='{surveyLink}'>Clique aqui</a></p>");
    }
}
