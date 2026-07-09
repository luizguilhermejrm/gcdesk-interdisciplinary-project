using System;
using System.Collections.Generic;
using System.Data;

/// <summary>Data access layer for the Notification entity.</summary>
public class NotificationRepository : IRepository<Notification>
{
    /// <summary>Retrieves a notification by its primary key.</summary>
    public Notification GetById(int id)
    {
        return DataHelper.ExecuteReader("SELECT * FROM notification WHERE not_id = ?id",
            r => Map(r), Mapped.Parameter("?id", id))[0];
    }

    /// <summary>Returns all non-deleted notifications.</summary>
    public List<Notification> GetAll()
    {
        return DataHelper.ExecuteReader("SELECT * FROM notification WHERE deleted_at IS NULL", r => Map(r));
    }

    /// <summary>Inserts a new notification.</summary>
    public int Insert(Notification entity)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            @"INSERT INTO notification (not_description, not_title, tic_id, not_timeMensage, not_status, created_at, updated_at)
              VALUES (?desc, ?title, ?tic, ?time, ?status, ?now, ?now)",
            Mapped.Parameter("?desc", entity.Description),
            Mapped.Parameter("?title", entity.Title),
            Mapped.Parameter("?tic", entity.TicketId),
            Mapped.Parameter("?time", now),
            Mapped.Parameter("?status", 0),
            Mapped.Parameter("?now", now));
    }

    /// <summary>Updates a notification's status.</summary>
    public bool Update(Notification entity)
    {
        return DataHelper.ExecuteNonQuery(
            "UPDATE notification SET not_status = ?status, updated_at=?now WHERE not_id = ?id",
            Mapped.Parameter("?status", entity.NotficationId),
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", entity.NotficationId)) > 0;
    }

    /// <summary>Soft-deletes a notification by its primary key.</summary>
    public bool Delete(int id)
    {
        return DataHelper.ExecuteNonQuery("UPDATE notification SET deleted_at=?now WHERE not_id = ?id",
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", id)) > 0;
    }

    /// <summary>Returns non-deleted notifications for a collaborator, joined with ticket info.</summary>
    public static DataSet SelectNotification(int id)
    {
        return DataHelper.FillDataSet(
            "SELECT * FROM notification A INNER JOIN ticket B ON A.tic_id = B.tic_id AND B.deleted_at IS NULL WHERE user_id = ?cod AND A.deleted_at IS NULL ORDER BY not_id DESC",
            Mapped.Parameter("?cod", id));
    }

    /// <summary>Returns non-deleted notifications for an analyst, joined with ticket info.</summary>
    public static DataSet SelectNotificationAnalisty(int id)
    {
        return DataHelper.FillDataSet(
            "SELECT * FROM notification A INNER JOIN ticket B ON A.tic_id = B.tic_id AND B.deleted_at IS NULL WHERE ana_analisty_id = ?cod AND not_status != 0 AND A.deleted_at IS NULL ORDER BY not_id DESC",
            Mapped.Parameter("?cod", id));
    }

    /// <summary>Creates a "ticket finished" notification.</summary>
    public int InsertNotificationStatusFinished(int idTicket, string timeMessage)
    {
        try
        {
            string now = Function.Now();
            DataHelper.ExecuteNonQuery(
                @"INSERT INTO notification (not_description, not_title, tic_id, not_timeMensage, not_status, created_at, updated_at)
                  VALUES ('O seu chamado foi finalizado, poderia nos enviar uma avaliação por favor.',
                          'Chamado Finalizado', ?tic_id, ?timeMessage, '2', ?now, ?now)",
                Mapped.Parameter("?timeMessage", timeMessage),
                Mapped.Parameter("?tic_id", idTicket),
                Mapped.Parameter("?now", now));
            return 0;
        }
        catch
        {
            return -2;
        }
    }

    /// <summary>Returns the ID of the most recently inserted ticket.</summary>
    public string SelectIdTicket()
    {
        return DataHelper.ExecuteScalar<int>("SELECT MAX(tic_id) ULTIMO_ID FROM ticket").ToString();
    }

    /// <summary>Creates a "ticket created" notification for the latest ticket.</summary>
    public int InsertNotificationStatusCreate(string timeMessage)
    {
        try
        {
            string idTicket = SelectIdTicket();
            string now = Function.Now();
            DataHelper.ExecuteNonQuery(
                @"INSERT INTO notification (not_description, not_title, tic_id, not_timeMensage, not_status, created_at, updated_at)
                  VALUES ('O seu chamado foi criado com sucesso.',
                          'Chamado Criado', ?tic_id, ?timeMessage, 0, ?now, ?now)",
                Mapped.Parameter("?tic_id", idTicket),
                Mapped.Parameter("?timeMessage", timeMessage),
                Mapped.Parameter("?now", now));
            return 0;
        }
        catch
        {
            return -2;
        }
    }

    /// <summary>Maps the current IDataReader row to a Notification object.</summary>
    private static Notification Map(IDataReader r)
    {
        return new Notification
        {
            NotficationId = Convert.ToInt32(r["not_id"]),
            Description = r["not_description"].ToString(),
            Title = r["not_title"].ToString(),
            TicketId = Convert.ToInt32(r["tic_id"]),
            CreatedAt = r["created_at"] == DBNull.Value ? null : r["created_at"].ToString(),
            UpdatedAt = r["updated_at"] == DBNull.Value ? null : r["updated_at"].ToString(),
            DeletedAt = r["deleted_at"] == DBNull.Value ? null : r["deleted_at"].ToString()
        };
    }
}
