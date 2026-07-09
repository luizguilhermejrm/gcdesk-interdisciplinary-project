using System;
using System.Data;

/// <summary>Business logic service for ticket operations.</summary>
public class TicketService
{
    private readonly TicketRepository _ticketRepository = new TicketRepository();
    private readonly NotificationService _notificationService = new NotificationService();

    /// <summary>Creates a new ticket, auto-assigns based on skill, and records history.</summary>
    public int Create(Ticket ticket)
    {
        int result = TicketRepository.Insert(ticket);
        if (result == 0)
        {
            int newId = DataHelper.ExecuteScalar<int>("SELECT MAX(tic_id) FROM ticket");
            HistoryRepository.Insert(newId, ticket.UserId, "CRIADO", "Chamado criado");
            int analystId = TicketRepository.FindAnalystBySkill(ticket.TypeTicket);
            if (analystId > 0)
            {
                _ticketRepository.UpdateTicketAnaSt(analystId, newId);
                HistoryRepository.Insert(newId, analystId, "ACEITO", "Chamado automaticamente alocado por skill");
            }
        }
        return result;
    }

    /// <summary>Returns reopened ticket counts grouped by type (bottleneck metrics).</summary>
    public DataSet GetReopenByType()
    {
        return TicketRepository.GetReopenByType();
    }

    /// <summary>Returns SLA-breached tickets.</summary>
    public DataSet GetSlaBreached()
    {
        return TicketRepository.GetSlaBreached();
    }

    /// <summary>Returns tickets nearing auto-closure (finished, no rating, 48h+).</summary>
    public DataSet GetTicketsAwaitingAutoClose()
    {
        return TicketRepository.GetTicketsAwaitingAutoClose();
    }

    /// <summary>Increments reopen count when a finished ticket is reopened.</summary>
    public void IncrementReopenCount(int ticketId)
    {
        DataHelper.ExecuteNonQuery(
            "UPDATE ticket SET reopen_count = reopen_count + 1, tic_status = 0, ana_analisty_id = NULL, updated_at=?now WHERE tic_id = ?id",
            Mapped.Parameter("?id", ticketId),
            Mapped.Parameter("?now", Function.Now()));
    }

    /// <summary>Returns all open tickets (status = 0).</summary>
    public DataSet GetAllOpen()
    {
        return TicketRepository.SelectTicketOpen();
    }

    /// <summary>Returns all tickets for the analyst view (with user names).</summary>
    public DataSet GetAllForAnalyst()
    {
        return TicketRepository.SelectTicketAllAnalyst();
    }

    /// <summary>Searches tickets by description or analyst name.</summary>
    public DataSet SearchAllForAnalyst(string term)
    {
        return TicketRepository.SearchTicketAllAnalyst(term);
    }

    /// <summary>Returns tickets submitted by a specific collaborator.</summary>
    public DataSet GetByCollaboratorId(int userId)
    {
        return TicketRepository.SelectTicketCollaborator(userId);
    }

    /// <summary>Returns tickets assigned to a specific analyst.</summary>
    public DataSet GetByAnalystId(int analystId)
    {
        return TicketRepository.SelectTicketAna(analystId);
    }

    /// <summary>Returns a single ticket by ID as a DataSet.</summary>
    public DataSet GetById(int ticketId)
    {
        return _ticketRepository.SelectOne(ticketId.ToString());
    }

    /// <summary>Returns a single Ticket object by ID.</summary>
    public Ticket GetTicketById(int ticketId)
    {
        return _ticketRepository.GetById(ticketId);
    }

    /// <summary>Assigns a ticket to an analyst and logs the event.</summary>
    public int Claim(int analystId, int ticketId)
    {
        string timeMessage = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");
        int result = _ticketRepository.UpdateTicketAnaSt(analystId, ticketId);
        _notificationService.InsertNotificationStatusProgress(ticketId, timeMessage);
        HistoryRepository.Insert(ticketId, analystId, "ACEITO", "Chamado aceito pelo analista");
        return result;
    }

    /// <summary>Updates the status of a ticket (open/progress/finished).</summary>
    public int UpdateStatus(int status, int ticketId, string closeTime)
    {
        return TicketRepository.UpdateTicket(status, ticketId, closeTime);
    }

    /// <summary>Finishes a ticket (status = 2), records history and notifies the requester.</summary>
    public int Finish(int ticketId)
    {
        string closeTime = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");
        _notificationService.InsertNotificationStatusFinished(ticketId, closeTime);
        int result = TicketRepository.UpdateTicket(2, ticketId, closeTime);
        HistoryRepository.Insert(ticketId, 0, "FINALIZADO", "Chamado finalizado");
        return result;
    }

    /// <summary>Returns the count of open tickets for a collaborator as a string.</summary>
    public string CountOpenByCollaborator(int userId)
    {
        return Convert.ToString(_ticketRepository.SelectOpen(userId));
    }

    /// <summary>Returns the count of in-progress tickets for a collaborator as a string.</summary>
    public string CountProgressByCollaborator(int userId)
    {
        return Convert.ToString(_ticketRepository.SelectProgress(userId));
    }

    /// <summary>Returns the count of finished tickets for a collaborator as a string.</summary>
    public string CountFinishedByCollaborator(int userId)
    {
        return Convert.ToString(_ticketRepository.SelectFinished(userId));
    }

    /// <summary>Returns the count of in-progress tickets for an analyst as a string.</summary>
    public string CountProgressByAnalyst(int analystId)
    {
        return Convert.ToString(_ticketRepository.SelectProgressAnalyst(analystId));
    }

    /// <summary>Returns the count of finished tickets for an analyst as a string.</summary>
    public string CountFinishedByAnalyst(int analystId)
    {
        return Convert.ToString(_ticketRepository.SelectFinishedAnalyst(analystId));
    }

    /// <summary>Returns the total count of open tickets as a string.</summary>
    public string CountAllOpen()
    {
        return Convert.ToString(_ticketRepository.SelectAllOpen());
    }

    /// <summary>Returns ticket counts grouped by department.</summary>
    public DataSet GetTicketsByDepartment()
    {
        return TicketRepository.GetTicketsByDepartment();
    }

    /// <summary>Returns ticket counts grouped by status.</summary>
    public DataSet GetTicketsByStatus()
    {
        return TicketRepository.GetTicketsByStatus();
    }

    /// <summary>Returns ticket counts grouped by day for the last N days.</summary>
    public DataSet GetTicketsByPeriod(int days)
    {
        return TicketRepository.GetTicketsByPeriod(days);
    }

    /// <summary>Returns the top-rated tickets (rating > 0, ordered descending).</summary>
    public DataSet GetTopRatedTickets()
    {
        return TicketRepository.GetTopRatedTickets();
    }

    /// <summary>Updates the rating for a finished ticket.</summary>
    public int UpdateRating(int ticketId, int rating, string comment)
    {
        return TicketRepository.UpdateRating(ticketId, rating, comment);
    }
}
