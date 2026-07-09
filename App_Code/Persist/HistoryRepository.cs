using System;
using System.Collections.Generic;
using System.Data;

/// <summary>Data access layer for ticket history / timeline events.</summary>
public class HistoryRepository
{
    /// <summary>Inserts a new history event for a ticket.</summary>
    public static void Insert(int ticketId, int userId, string action, string description)
    {
        string now = Function.Now();
        DataHelper.ExecuteNonQuery(
            "INSERT INTO ticket_history (tic_id, user_id, action, description, created_at, updated_at) VALUES (?tic, ?uid, ?action, ?desc, ?now, ?now)",
            Mapped.Parameter("?tic", ticketId),
            Mapped.Parameter("?uid", userId),
            Mapped.Parameter("?action", action),
            Mapped.Parameter("?desc", description),
            Mapped.Parameter("?now", now));
    }

    /// <summary>Returns all history events for a ticket, ordered chronologically.</summary>
    public static DataSet GetByTicketId(int ticketId)
    {
        return DataHelper.FillDataSet(
            @"SELECT H.*, U.user_name FROM ticket_history H
              LEFT JOIN user U ON H.user_id = U.user_id AND U.deleted_at IS NULL
              WHERE H.tic_id = ?tic ORDER BY H.history_id ASC",
            Mapped.Parameter("?tic", ticketId));
    }
}
