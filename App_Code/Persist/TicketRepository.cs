using System;
using System.Collections.Generic;
using System.Data;

/// <summary>Data access layer for the Ticket entity.</summary>
public class TicketRepository : IRepository<Ticket>
{
    /// <summary>Maps the current IDataReader row to a Ticket object.</summary>
    private static Ticket Map(IDataReader r)
    {
        Ticket t = new Ticket
        {
            TicketId = Convert.ToInt32(r["tic_id"]),
            Description = r["tic_description"].ToString(),
            Localization = r["tic_localization"].ToString(),
            TypeTicket = r["tic_type"].ToString(),
            Status = Convert.ToInt32(r["tic_status"]),
            OpenTime = r["tic_openTime"].ToString(),
            CloseTime = r["tic_closeTime"].ToString(),
            UserId = Convert.ToInt32(r["user_id"]),
            AnalystId = r["ana_analisty_id"] != DBNull.Value ? Convert.ToInt32(r["ana_analisty_id"]) : 0,
            EquipId = HasColumn(r, "equip_id") ? Convert.ToInt32(r["equip_id"]) : 0
        };
        t.Priority = HasColumn(r, "tic_priority") ? Convert.ToInt32(r["tic_priority"]) : 0;
        t.Grade = HasColumn(r, "grade") ? Convert.ToInt32(r["grade"]) : 0;
        t.Rating = HasColumn(r, "rating") ? Convert.ToInt32(r["rating"]) : 0;
        t.RatingComment = HasColumn(r, "rating_comment") ? r["rating_comment"].ToString() : null;
        t.RatingAt = HasColumn(r, "rating_at") ? r["rating_at"].ToString() : null;
        t.SlaBreached = HasColumn(r, "sla_breached") ? Convert.ToInt32(r["sla_breached"]) : 0;
        t.LastResponseAt = HasColumn(r, "last_response_at") ? r["last_response_at"].ToString() : null;
        t.ReopenCount = HasColumn(r, "reopen_count") ? Convert.ToInt32(r["reopen_count"]) : 0;
        t.CreatedAt = HasColumn(r, "created_at") ? (r["created_at"] == DBNull.Value ? null : r["created_at"].ToString()) : null;
        t.UpdatedAt = HasColumn(r, "updated_at") ? (r["updated_at"] == DBNull.Value ? null : r["updated_at"].ToString()) : null;
        t.DeletedAt = HasColumn(r, "deleted_at") ? (r["deleted_at"] == DBNull.Value ? null : r["deleted_at"].ToString()) : null;
        return t;
    }

    /// <summary>Checks if a column exists in the IDataReader by name (case-insensitive).</summary>
    private static bool HasColumn(IDataReader r, string name)
    {
        for (int i = 0; i < r.FieldCount; i++)
            if (r.GetName(i).Equals(name, StringComparison.OrdinalIgnoreCase))
                return true;
        return false;
    }

    /// <summary>Retrieves a ticket by its primary key (includes deleted for detail view).</summary>
    public Ticket GetById(int id)
    {
        return DataHelper.ExecuteReader(
            "SELECT * FROM ticket WHERE tic_id=?id",
            r => Map(r),
            Mapped.Parameter("?id", id))[0];
    }

    /// <summary>Returns all active (non-deleted) tickets.</summary>
    public List<Ticket> GetAll()
    {
        return DataHelper.ExecuteReader("SELECT * FROM ticket WHERE deleted_at IS NULL", r => Map(r));
    }

    /// <summary>Returns all open tickets (status = 0, not deleted).</summary>
    public static DataSet SelectTicketOpen()
    {
        return DataHelper.FillDataSet("SELECT * FROM ticket WHERE tic_status=0 AND deleted_at IS NULL");
    }

    /// <summary>Searches active tickets by description or analyst name for the analyst view.</summary>
    public static DataSet SearchTicketAllAnalyst(string term)
    {
        string sql = @"SELECT A.tic_id, B.user_name, A.tic_description, A.tic_localization,
                       A.tic_openTime, A.tic_closeTime, A.tic_status,
                       A.tic_priority, A.sla_breached
                       FROM ticket A INNER JOIN user B ON A.ana_analisty_id = B.user_id
                       WHERE (A.tic_description LIKE ?term OR B.user_name LIKE ?term)
                       AND A.deleted_at IS NULL";
        return DataHelper.FillDataSet(sql, Mapped.Parameter("?term", $"%{term}%"));
    }

    /// <summary>Returns all active tickets with analyst names for the analyst view.</summary>
    public static DataSet SelectTicketAllAnalyst()
    {
        return DataHelper.FillDataSet(
            @"SELECT A.tic_id, B.user_name, A.tic_description, A.tic_localization,
              A.tic_openTime, A.tic_closeTime, A.tic_status,
              A.tic_priority, A.sla_breached
              FROM ticket A INNER JOIN user B ON A.ana_analisty_id = B.user_id
              WHERE A.deleted_at IS NULL");
    }

    /// <summary>Returns all active tickets submitted by a specific collaborator.</summary>
    public static DataSet SelectTicketCollaborator(int id)
    {
        return DataHelper.FillDataSet(
            "SELECT * FROM ticket WHERE user_id=?id AND deleted_at IS NULL ORDER BY tic_id DESC",
            Mapped.Parameter("?id", id));
    }

    /// <summary>Returns all active tickets assigned to a specific analyst.</summary>
    public static DataSet SelectTicketAna(int id)
    {
        return DataHelper.FillDataSet(
            "SELECT * FROM ticket WHERE ana_analisty_id=?id AND deleted_at IS NULL",
            Mapped.Parameter("?id", id));
    }

    /// <summary>Explicit interface implementation — delegates to static Insert.</summary>
    int IRepository<Ticket>.Insert(Ticket entity)
    {
        return Insert(entity);
    }

    /// <summary>Inserts a new ticket into the database.</summary>
    public static int Insert(Ticket t)
    {
        try
        {
            string now = Function.Now();
            return DataHelper.ExecuteNonQuery(
                @"INSERT INTO ticket (tic_description, tic_localization, tic_type, tic_priority,
                  tic_status, tic_openTime, grade, rating, user_id, equip_id, created_at, updated_at)
                  VALUES (?description, ?localization, ?type, ?priority, 0, ?openTime, 0, 0, ?id, ?equip, ?now, ?now)",
                Mapped.Parameter("?description", t.Description),
                Mapped.Parameter("?localization", t.Localization),
                Mapped.Parameter("?type", t.TypeTicket),
                Mapped.Parameter("?priority", t.Priority),
                Mapped.Parameter("?openTime", t.OpenTime),
                Mapped.Parameter("?id", t.UserId),
                Mapped.Parameter("?equip", t.EquipId > 0 ? t.EquipId : (object)DBNull.Value),
                Mapped.Parameter("?now", now));
        }
        catch
        {
            return -2;
        }
    }

    /// <summary>Helper for count queries: builds COUNT with the given WHERE clause and deleted_at IS NULL.</summary>
    private string CountQuery(string where, params IDbDataParameter[] parameters)
    {
        int total = DataHelper.ExecuteScalar<int>(
            $"SELECT COUNT(tic_id) FROM ticket WHERE {where} AND deleted_at IS NULL", parameters);
        return total.ToString();
    }

    /// <summary>Returns the count of finished tickets for a collaborator.</summary>
    public string SelectFinished(int id)
    {
        return CountQuery("user_id=?id AND tic_status=2", Mapped.Parameter("?id", id));
    }

    /// <summary>Returns the count of in-progress tickets for a collaborator.</summary>
    public string SelectProgress(int id)
    {
        return CountQuery("user_id=?id AND tic_status=1", Mapped.Parameter("?id", id));
    }

    /// <summary>Returns the count of open tickets for a collaborator.</summary>
    public string SelectOpen(int id)
    {
        return CountQuery("user_id=?id AND tic_status=0", Mapped.Parameter("?id", id));
    }

    /// <summary>Returns the count of in-progress tickets for an analyst.</summary>
    public string SelectProgressAnalyst(int id)
    {
        return CountQuery("ana_analisty_id=?id AND tic_status=1", Mapped.Parameter("?id", id));
    }

    /// <summary>Returns the count of finished tickets for an analyst.</summary>
    public string SelectFinishedAnalyst(int id)
    {
        return CountQuery("ana_analisty_id=?id AND tic_status=2", Mapped.Parameter("?id", id));
    }

    /// <summary>Returns the total count of open tickets (status = 0).</summary>
    public string SelectAllOpen()
    {
        return CountQuery("tic_status=0");
    }

    /// <summary>Returns a single ticket row as a DataSet.</summary>
    public DataSet SelectOne(string ticketID)
    {
        return DataHelper.FillDataSet(
            "SELECT * FROM ticket WHERE tic_id=?cod AND deleted_at IS NULL",
            Mapped.Parameter("?cod", ticketID));
    }

    /// <summary>Explicit interface implementation — delegates to static UpdateTicket.</summary>
    bool IRepository<Ticket>.Update(Ticket entity)
    {
        return UpdateTicket(entity.Status, entity.TicketId, entity.CloseTime) == 0;
    }

    /// <summary>Updates the status and close time of a ticket.</summary>
    public static int UpdateTicket(int value, int idTicket, string closeTime)
    {
        try
        {
            return DataHelper.ExecuteNonQuery(
                "UPDATE ticket SET tic_status=?value, tic_closeTime=?close, updated_at=?now WHERE tic_id=?idTicket",
                Mapped.Parameter("?value", value),
                Mapped.Parameter("?idTicket", idTicket),
                Mapped.Parameter("?close", closeTime),
                Mapped.Parameter("?now", Function.Now()));
        }
        catch
        {
            return -2;
        }
    }

    /// <summary>Explicit interface implementation — delegates to static Delete.</summary>
    bool IRepository<Ticket>.Delete(int id)
    {
        return Delete(id);
    }

    /// <summary>Soft-deletes a ticket by ID (sets deleted_at).</summary>
    public static bool Delete(int id)
    {
        try
        {
            DataHelper.ExecuteNonQuery("UPDATE ticket SET deleted_at=?now WHERE tic_id=?id",
                Mapped.Parameter("?now", Function.Now()),
                Mapped.Parameter("?id", id));
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>Assigns a ticket to an analyst and sets status to in progress (1).</summary>
    public int UpdateTicketAnaSt(int anaid, int idTicket)
    {
        try
        {
            return DataHelper.ExecuteNonQuery(
                "UPDATE ticket SET tic_status='1', ana_analisty_id=?analista, updated_at=?now WHERE tic_id=?idTicket",
                Mapped.Parameter("?analista", anaid),
                Mapped.Parameter("?now", Function.Now()),
                Mapped.Parameter("?idTicket", idTicket));
        }
        catch
        {
            return -2;
        }
    }

    /// <summary>Returns ticket counts grouped by department for charting.</summary>
    public static DataSet GetTicketsByDepartment()
    {
        return DataHelper.FillDataSet(
            @"SELECT D.dep_sector, COUNT(T.tic_id) AS total
              FROM ticket T INNER JOIN user U ON T.user_id = U.user_id AND T.deleted_at IS NULL AND U.deleted_at IS NULL
              RIGHT JOIN department D ON U.dep_id = D.dep_id
              GROUP BY D.dep_sector");
    }

    /// <summary>Returns ticket counts grouped by status for charting.</summary>
    public static DataSet GetTicketsByStatus()
    {
        return DataHelper.FillDataSet(
            @"SELECT CASE tic_status
                WHEN 0 THEN 'Aberto'
                WHEN 1 THEN 'Em Andamento'
                WHEN 2 THEN 'Finalizado'
              END AS status_name, COUNT(*) AS total
              FROM ticket WHERE deleted_at IS NULL GROUP BY tic_status");
    }

    /// <summary>Returns ticket counts grouped by day for the last N days.</summary>
    public static DataSet GetTicketsByPeriod(int days)
    {
        return DataHelper.FillDataSet(
            @"SELECT DATE(STR_TO_DATE(tic_openTime, '%d/%m/%Y %H:%i:%s')) AS dia,
              COUNT(*) AS total
              FROM ticket
              WHERE deleted_at IS NULL
              AND STR_TO_DATE(tic_openTime, '%d/%m/%Y %H:%i:%s') >= DATE_SUB(NOW(), INTERVAL ?days DAY)
              GROUP BY dia ORDER BY dia",
            Mapped.Parameter("?days", days));
    }

    /// <summary>Returns top 10 rated tickets for reports.</summary>
    public static DataSet GetTopRatedTickets()
    {
        return DataHelper.FillDataSet(
            @"SELECT T.tic_id, T.tic_description, T.rating, T.rating_comment,
              U.user_name FROM ticket T
              INNER JOIN user U ON T.user_id = U.user_id
              WHERE T.rating > 0 AND T.deleted_at IS NULL ORDER BY T.rating DESC LIMIT 10");
    }

    /// <summary>Updates the rating fields on a finished ticket.</summary>
    public static int UpdateRating(int ticketId, int rating, string comment)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            "UPDATE ticket SET rating = ?r, rating_comment = ?c, rating_at = ?now, updated_at = ?now WHERE tic_id = ?id",
            Mapped.Parameter("?r", rating),
            Mapped.Parameter("?c", comment),
            Mapped.Parameter("?now", now),
            Mapped.Parameter("?id", ticketId));
    }

    /// <summary>Returns reopened ticket counts grouped by type (bottleneck metrics).</summary>
    public static DataSet GetReopenByType()
    {
        return DataHelper.FillDataSet(
            @"SELECT tic_type, COUNT(*) AS total, SUM(reopen_count) AS total_reopens
              FROM ticket WHERE reopen_count > 0 AND deleted_at IS NULL
              GROUP BY tic_type ORDER BY total_reopens DESC");
    }

    /// <summary>Returns SLA-breached ticket counts for dashboard alerts.</summary>
    public static DataSet GetSlaBreached()
    {
        return DataHelper.FillDataSet(
            "SELECT * FROM ticket WHERE sla_breached = 1 AND tic_status < 2 AND deleted_at IS NULL");
    }

    /// <summary>Finds an available analyst specialized in the given ticket type (skill-based routing).</summary>
    public static int FindAnalystBySkill(string ticketType)
    {
        string sql = @"SELECT U.user_id FROM user U
            INNER JOIN user_skill S ON U.user_id = S.user_id
            WHERE S.skill_name = ?type AND U.user_typeAccess = 0 AND U.user_status = 1
            AND S.deleted_at IS NULL AND U.deleted_at IS NULL
            AND U.user_id NOT IN (
                SELECT ana_analisty_id FROM ticket
                WHERE tic_status = 1 AND ana_analisty_id IS NOT NULL AND deleted_at IS NULL
                GROUP BY ana_analisty_id HAVING COUNT(*) >= 3
            )
            ORDER BY (
                SELECT COUNT(*) FROM ticket WHERE ana_analisty_id = U.user_id AND tic_status = 1 AND deleted_at IS NULL
            ) ASC LIMIT 1";
        var result = DataHelper.ExecuteReader(sql, r => Convert.ToInt32(r["user_id"]),
            Mapped.Parameter("?type", ticketType));
        return result.Count > 0 ? result[0] : 0;
    }

    /// <summary>Returns tickets nearing SLA breach (threshold in hours).</summary>
    public static DataSet GetTicketsNearSlaBreach(int priority, int limitHours)
    {
        return DataHelper.FillDataSet(
            @"SELECT T.* FROM ticket T
              WHERE T.tic_priority >= ?priority AND T.tic_status < 2 AND T.sla_breached = 0 AND T.deleted_at IS NULL
              AND TIMESTAMPDIFF(HOUR, STR_TO_DATE(T.tic_openTime, '%d/%m/%Y %H:%i:%s'), NOW()) >= ?hours",
            Mapped.Parameter("?priority", priority),
            Mapped.Parameter("?hours", limitHours));
    }

    /// <summary>Tickets awaiting closure (finished + no response in 48h or no rating).</summary>
    public static DataSet GetTicketsAwaitingAutoClose()
    {
        return DataHelper.FillDataSet(
            @"SELECT * FROM ticket WHERE tic_status = 2 AND rating = 0 AND deleted_at IS NULL
              AND STR_TO_DATE(tic_closeTime, '%d/%m/%Y %H:%i:%s') <= DATE_SUB(NOW(), INTERVAL 48 HOUR)");
    }

    /// <summary>Creates a "ticket in progress" notification record.</summary>
    public int InsertNotificationStatusProgress(int idTicket, string timeMessage)
    {
        try
        {
            string now = Function.Now();
            return DataHelper.ExecuteNonQuery(
                @"INSERT INTO notification (not_description, not_title, tic_id, not_timeMensage, not_Status, created_at, updated_at)
                  VALUES ('O seu chamado foi aceito, a partir de agora está sendo desenvolvido pelo analista',
                          'Chamado em Andamento', ?tic_id, ?timeMessage, '1', ?now, ?now)",
                Mapped.Parameter("?timeMessage", timeMessage),
                Mapped.Parameter("?tic_id", idTicket),
                Mapped.Parameter("?now", now));
        }
        catch
        {
            return -2;
        }
    }
}
