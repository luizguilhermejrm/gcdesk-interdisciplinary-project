using System;
using System.Data;

/// <summary>Business logic service for notification operations.</summary>
public class NotificationService
{
    private readonly NotificationRepository _notificationRepository = new NotificationRepository();
    private readonly TicketRepository _ticketRepository = new TicketRepository();

    /// <summary>Returns notifications for a collaborator.</summary>
    public DataSet GetByCollaboratorId(int userId)
    {
        return NotificationRepository.SelectNotification(userId);
    }

    /// <summary>Returns notifications for an analyst.</summary>
    public DataSet GetByAnalystId(int analystId)
    {
        return NotificationRepository.SelectNotificationAnalisty(analystId);
    }

    /// <summary>Creates a "ticket created" notification.</summary>
    public void InsertNotificationStatusCreate(string timeMessage)
    {
        _notificationRepository.InsertNotificationStatusCreate(timeMessage);
    }

    /// <summary>Creates a "ticket in progress" notification.</summary>
    public void InsertNotificationStatusProgress(int ticketId, string timeMessage)
    {
        _ticketRepository.InsertNotificationStatusProgress(ticketId, timeMessage);
    }

    /// <summary>Creates a "ticket finished" notification.</summary>
    public void InsertNotificationStatusFinished(int ticketId, string timeMessage)
    {
        _notificationRepository.InsertNotificationStatusFinished(ticketId, timeMessage);
    }
}
