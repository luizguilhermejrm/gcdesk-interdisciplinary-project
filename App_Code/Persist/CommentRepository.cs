using System;
using System.Data;

/// <summary>Data access layer for ticket comments.</summary>
public class CommentRepository
{
    /// <summary>Inserts a new comment on a ticket.</summary>
    public static void Insert(int ticketId, int userId, string text)
    {
        string now = Function.Now();
        DataHelper.ExecuteNonQuery(
            "INSERT INTO ticket_comments (tic_id, user_id, comment_text, created_at, updated_at) VALUES (?tic, ?uid, ?text, ?now, ?now)",
            Mapped.Parameter("?tic", ticketId),
            Mapped.Parameter("?uid", userId),
            Mapped.Parameter("?text", text),
            Mapped.Parameter("?now", now));
    }

    /// <summary>Returns all comments for a ticket, ordered by creation time.</summary>
    public static DataSet GetByTicketId(int ticketId)
    {
        return DataHelper.FillDataSet(
            @"SELECT C.*, U.user_name, U.user_image FROM ticket_comments C
              LEFT JOIN user U ON C.user_id = U.user_id AND U.deleted_at IS NULL
              WHERE C.tic_id = ?tic ORDER BY C.comment_id ASC",
            Mapped.Parameter("?tic", ticketId));
    }
}
