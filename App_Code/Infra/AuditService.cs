using System;
using System.Web;

/// <summary>Provides auditing capabilities by writing events to the audit_log table.</summary>
public static class AuditService
{
    /// <summary>Writes an audit log entry.</summary>
    /// <param name="userId">The ID of the user performing the action.</param>
    /// <param name="action">Action name (e.g. "CLAIM_TICKET", "CSRF_INVALID").</param>
    /// <param name="detail">Additional details (e.g. request URL).</param>
    public static void Log(string userId, string action, string detail)
    {
        try
        {
            DataHelper.ExecuteNonQuery(
                @"INSERT INTO audit_log (user_id, action, detail, ip_address, created_at, updated_at)
                  VALUES (?uid, ?action, ?detail, ?ip, ?now, ?now)",
                Mapped.Parameter("?uid", userId),
                Mapped.Parameter("?action", action),
                Mapped.Parameter("?detail", detail),
                Mapped.Parameter("?ip", GetIP()),
                Mapped.Parameter("?now", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }
        catch
        {
        }
    }

    /// <summary>Extracts the client IP address, respecting X-Forwarded-For headers.</summary>
    private static string GetIP()
    {
        var ctx = HttpContext.Current;
        if (ctx == null) return "0.0.0.0";
        string ip = ctx.Request.Headers["X-Forwarded-For"];
        if (string.IsNullOrEmpty(ip))
            ip = ctx.Request.UserHostAddress;
        return ip ?? "0.0.0.0";
    }
}
