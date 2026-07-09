using System;
using System.Data;

/// <summary>Data access layer for the audit_log table (read-only queries).</summary>
public class AuditRepository
{
    /// <summary>Returns all audit log entries ordered by most recent first.</summary>
    public static DataSet GetAll()
    {
        return DataHelper.FillDataSet(
            "SELECT * FROM audit_log ORDER BY created_at DESC, log_id DESC");
    }

    /// <summary>
    /// Returns audit log entries filtered by action, user id and date range.
    /// Null/empty parameters are ignored (no filter applied for that field).
    /// </summary>
    /// <param name="action">Action name to filter (e.g. "LOGIN", "CREATE_TICKET").</param>
    /// <param name="userId">User id to filter (logged as string in audit_log).</param>
    /// <param name="dateFrom">Start date (dd/MM/yyyy) or empty.</param>
    /// <param name="dateTo">End date (dd/MM/yyyy) or empty.</param>
    public static DataSet Search(string action, string userId, string dateFrom, string dateTo)
    {
        string sql = @"SELECT * FROM audit_log WHERE 1=1";
        if (!string.IsNullOrEmpty(action))
            sql += " AND action LIKE ?action";
        if (!string.IsNullOrEmpty(userId))
            sql += " AND user_id = ?userId";
        if (!string.IsNullOrEmpty(dateFrom))
            sql += " AND STR_TO_DATE(created_at, '%Y-%m-%d %H:%i:%s') >= STR_TO_DATE(?dateFrom, '%d/%m/%Y %H:%i:%s')";
        if (!string.IsNullOrEmpty(dateTo))
            sql += " AND STR_TO_DATE(created_at, '%Y-%m-%d %H:%i:%s') <= STR_TO_DATE(?dateTo, '%d/%m/%Y %H:%i:%s')";
        sql += " ORDER BY created_at DESC, log_id DESC";

        return DataHelper.FillDataSet(sql,
            Mapped.Parameter("?action", "%" + (action ?? "") + "%"),
            Mapped.Parameter("?userId", userId ?? ""),
            Mapped.Parameter("?dateFrom", dateFrom ?? ""),
            Mapped.Parameter("?dateTo", dateTo ?? ""));
    }

    /// <summary>Returns the distinct action names for populating filter dropdowns.</summary>
    public static DataSet GetDistinctActions()
    {
        return DataHelper.FillDataSet(
            "SELECT DISTINCT action FROM audit_log ORDER BY action ASC");
    }
}
