using System;
using System.Data;

public class SlaService
{
    public int GetResponseLimitHours(int priority)
    {
        SlaConfig config = SlaConfigRepository.GetByPriority(priority);
        return config?.ResponseLimitHours ?? 48;
    }

    public int GetEscalationHours(int priority)
    {
        SlaConfig config = SlaConfigRepository.GetByPriority(priority);
        return config?.EscalationHours ?? 0;
    }

    public void CheckAndFlagSlaBreaches()
    {
        DataSet nearBreach = TicketRepository.GetTicketsNearSlaBreach(0, 48);
        foreach (DataRow row in nearBreach.Tables[0].Rows)
        {
            int ticketId = Convert.ToInt32(row["tic_id"]);
            DataHelper.ExecuteNonQuery(
                "UPDATE ticket SET sla_breached = 1, updated_at=?now WHERE tic_id = ?id",
                Mapped.Parameter("?now", Function.Now()),
                Mapped.Parameter("?id", ticketId));
        }
    }
}
