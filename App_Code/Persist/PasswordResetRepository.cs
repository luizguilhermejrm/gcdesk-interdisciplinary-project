using System;
using System.Collections.Generic;
using System.Data;

public static class PasswordResetRepository
{
    public static int Insert(int userId, string token, string expiresAt)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            "INSERT INTO password_resets (user_id, token, expires_at, created_at, updated_at) VALUES (?uid, ?token, ?exp, ?now, ?now)",
            Mapped.Parameter("?uid", userId),
            Mapped.Parameter("?token", token),
            Mapped.Parameter("?exp", expiresAt),
            Mapped.Parameter("?now", now));
    }

    public static List<PasswordResetRow> GetValidToken(string token)
    {
        return DataHelper.ExecuteReader(
            "SELECT * FROM password_resets WHERE token = ?token AND used = 0 AND deleted_at IS NULL AND expires_at > ?now",
            r => new PasswordResetRow
            {
                UserId = Convert.ToInt32(r["user_id"]),
                ResetId = Convert.ToInt32(r["reset_id"])
            },
            Mapped.Parameter("?token", token),
            Mapped.Parameter("?now", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss")));
    }

    public static int UpdatePassword(int userId, string hashedPassword)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            "UPDATE user SET user_password = ?pass, updated_at=?now WHERE user_id = ?id",
            Mapped.Parameter("?pass", hashedPassword),
            Mapped.Parameter("?now", now),
            Mapped.Parameter("?id", userId));
    }

    public static int MarkUsed(int resetId)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            "UPDATE password_resets SET used = 1, updated_at=?now WHERE reset_id = ?id",
            Mapped.Parameter("?now", now),
            Mapped.Parameter("?id", resetId));
    }
}

public class PasswordResetRow
{
    public int UserId { get; set; }
    public int ResetId { get; set; }
}
