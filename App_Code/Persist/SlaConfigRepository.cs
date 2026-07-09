using System;
using System.Collections.Generic;
using System.Data;

public class SlaConfigRepository : IRepository<SlaConfig>
{
    private static SlaConfig Map(IDataReader r)
    {
        return new SlaConfig
        {
            SlaId = Convert.ToInt32(r["sla_id"]),
            Priority = Convert.ToInt32(r["tic_priority"]),
            ResponseLimitHours = Convert.ToInt32(r["response_limit_hours"]),
            ResolutionLimitHours = Convert.ToInt32(r["resolution_limit_hours"]),
            EscalationHours = Convert.ToInt32(r["escalation_hours"]),
            CreatedAt = r["created_at"] == DBNull.Value ? null : r["created_at"].ToString(),
            UpdatedAt = r["updated_at"] == DBNull.Value ? null : r["updated_at"].ToString(),
            DeletedAt = r["deleted_at"] == DBNull.Value ? null : r["deleted_at"].ToString()
        };
    }

    public SlaConfig GetById(int id)
    {
        var result = DataHelper.ExecuteReader(
            "SELECT * FROM sla_config WHERE sla_id=?id",
            r => Map(r),
            Mapped.Parameter("?id", id));
        return result.Count > 0 ? result[0] : null;
    }

    public List<SlaConfig> GetAll()
    {
        return DataHelper.ExecuteReader(
            "SELECT * FROM sla_config WHERE deleted_at IS NULL ORDER BY priority",
            r => Map(r));
    }

    public static SlaConfig GetByPriority(int priority)
    {
        var result = DataHelper.ExecuteReader(
            "SELECT * FROM sla_config WHERE tic_priority=?p AND deleted_at IS NULL",
            r => Map(r),
            Mapped.Parameter("?p", priority));
        return result.Count > 0 ? result[0] : null;
    }

    int IRepository<SlaConfig>.Insert(SlaConfig entity)
    {
        string now = Function.Now();
        return DataHelper.ExecuteNonQuery(
            "INSERT INTO sla_config (tic_priority, response_limit_hours, resolution_limit_hours, escalation_hours, created_at, updated_at)"
            + " VALUES (?p, ?r, ?res, ?esc, ?now, ?now)",
            Mapped.Parameter("?p", entity.Priority),
            Mapped.Parameter("?r", entity.ResponseLimitHours),
            Mapped.Parameter("?res", entity.ResolutionLimitHours),
            Mapped.Parameter("?esc", entity.EscalationHours),
            Mapped.Parameter("?now", now));
    }

    bool IRepository<SlaConfig>.Update(SlaConfig entity)
    {
        return DataHelper.ExecuteNonQuery(
            "UPDATE sla_config SET response_limit_hours=?r, resolution_limit_hours=?res, escalation_hours=?esc,"
            + " updated_at=?now WHERE sla_id=?id",
            Mapped.Parameter("?r", entity.ResponseLimitHours),
            Mapped.Parameter("?res", entity.ResolutionLimitHours),
            Mapped.Parameter("?esc", entity.EscalationHours),
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", entity.SlaId)) == 0;
    }

    bool IRepository<SlaConfig>.Delete(int id)
    {
        return DataHelper.ExecuteNonQuery(
            "UPDATE sla_config SET deleted_at=?now WHERE sla_id=?id",
            Mapped.Parameter("?now", Function.Now()),
            Mapped.Parameter("?id", id)) > 0;
    }
}
